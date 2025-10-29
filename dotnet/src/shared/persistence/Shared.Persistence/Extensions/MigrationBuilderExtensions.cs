using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;
using System.Reflection;

namespace Shared.Persistence.Extensions
{
    public static class MigrationBuilderExtensions
    {
        /// <summary>
        /// Создает View из вложенного SQL-ресурса, находящегося в указанной сборке.
        /// </summary>
        /// <param name="migrationBuilder">Экземпляр MigrationBuilder.</param>
        /// <param name="viewName">Имя View (и соответствующего .sql файла).</param>
        /// <param name="assemblyWithResource">Сборка, в которой находится встроенный SQL-ресурс.</param>
        /// <param name="resourcePrefix">Пространство имен до папки с SQL-файлами (например, "MyProject.Infrastructure.Persistence.Views").</param>
        public static void CreateViewFromResource(
            this MigrationBuilder migrationBuilder,
            string viewName,
            Assembly assemblyWithResource,
            string resourcePrefix)
        {
            var resourceName = $"{resourcePrefix}.{viewName}.sql";
            var sqlScript = GetSqlFromResource(assemblyWithResource, resourceName);
            migrationBuilder.Sql(sqlScript);
        }

        public static void DropView(this MigrationBuilder migrationBuilder, string viewName)
            => migrationBuilder.Sql($@"DROP VIEW IF EXISTS public.{viewName};");

        private static string GetSqlFromResource(Assembly assembly, string resourceName)
        {
            using var stream = assembly.GetManifestResourceStream(resourceName)
                ?? throw new InvalidOperationException($"Не удалось найти встроенный ресурс: {resourceName} в сборке {assembly.FullName}");

            using var reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }
    }
}