using KurrentDB.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NorthwindPlatform.Staff.Service.Application.Abstractions.Repositories;
using NorthwindPlatform.Staff.Service.Application.Models.Read;
using NorthwindPlatform.Staff.Service.Domain.Events.Employees;
using NorthwindPlatform.Staff.Service.Infrastructure.KurrentDB.Events;

namespace NorthwindPlatform.Staff.Service.Infrastructure.Projections.Services
{
    public sealed class EventStoreProjectionService(
        KurrentDBClient kurrentClient,
        IEventMapper eventMapper,
        IServiceScopeFactory scopeFactory
    ) : BackgroundService
    {
        private readonly KurrentDBClient _kurrentClient = kurrentClient;
        private readonly IEventMapper _eventMapper = eventMapper;
        private readonly IServiceScopeFactory _scopeFactory = scopeFactory;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var subscription = _kurrentClient.SubscribeToAllAsync(
                FromAll.Start,
                HandleEvent,
                subscriptionDropped: HandleDropped,
                cancellationToken: stoppingToken);

            try
            {
                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
            catch(OperationCanceledException)
            {
                
            }
            catch (System.Exception)
            {
                
            }
            finally
            {
                subscription.Dispose();
            }
        }

        private async Task HandleEvent(StreamSubscription subscription, ResolvedEvent resolvedEvent, CancellationToken cancellationToken)
        {
            if (resolvedEvent.Event.EventType.StartsWith("$"))
                return;

            if (!resolvedEvent.Event.EventStreamId.StartsWith("employee-"))
                return;

            try
            {
                var domainEvent = _eventMapper.MapToDomainEvent(resolvedEvent);

                if (domainEvent is null)
                    return;

                var streamId = resolvedEvent.Event.EventStreamId;
                var employeeId = Guid.Parse(streamId.Replace("employee-", ""));

                using var scope = _scopeFactory.CreateScope();
                var repo = scope.ServiceProvider.GetRequiredService<IEmployeeReadRepository>();

                await HandleDomainEventAsync(domainEvent, repo, cancellationToken);
            }
            catch (System.Exception)
            {
                throw;
            }         
        }

        private async Task HandleDomainEventAsync(object domainEvent, IEmployeeReadRepository repo, CancellationToken cancellationToken)
        {
            Func<Task> func = domainEvent switch
            {
                EmployeeCreated created => async () =>
                {
                    var employee = new EmployeeRead
                    {
                      Id = Guid.Parse(created.Id),
                      UserId = Guid.Parse(created.UserId),
                      AccountStatus = created.AccountStatus,
                      Name = created.Name,
                      Surname = created.Surname,
                      Patronymic = created.Patronymic,
                      DateOfBirth = DateOnly.Parse(created.DateOfBirth),
                      Gender = created.Gender,
                      Citizenship = created.Citizenship
                    };
                    
                    await repo.UpsertAsync(employee, cancellationToken);
                },
                _ => () => Task.CompletedTask
            };

            await func();
        }
        
        private void HandleDropped(StreamSubscription sub, SubscriptionDroppedReason reason, Exception? ex)
        {
            
        }
    }
}