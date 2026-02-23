using Shared.UI.Wpf.Enums;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Shared.UI.Wpf
{
    public sealed class PopupController : BindableBase
    {
        private readonly Func<bool>? _condition;

        public PopupController(
            PopupPlacementMode placementMode = PopupPlacementMode.Default, 
            bool staysOpen = true,
            Func<bool>? condition = null)
        {
            StaysOpen = staysOpen;

            Action action = placementMode switch
            {
                PopupPlacementMode.CustomRightUp => () => CustomPlacementCallback = PlacePopupRightUp,
                PopupPlacementMode.BottomCenter => () => CustomPlacementCallback = PlacePopupBottomCenter,
                _ => () => { }
            };

            action?.Invoke();

            if (condition != null)
                _condition = condition;

            ShowCommand = new DelegateCommand<UIElement>(Show, CanShow);
            ShowAtMouseCommand = new DelegateCommand(ShowAtMouse, () => true);
            HideCommand = new DelegateCommand(Hide, () => true);
        }

        /*--Свойства--------------------------------------------------------------------------------------*/

        private bool _isOpen;
        public bool IsOpen
        {
            get => _isOpen;
            set => SetProperty(ref _isOpen, value);
        }

        private bool _staysOpen;
        public bool StaysOpen
        {
            get => _staysOpen;
            set => SetProperty(ref _staysOpen, value);
        }

        private UIElement? _placementTarget;
        public UIElement? PlacementTarget
        {
            get => _placementTarget;
            set => SetProperty(ref _placementTarget, value);
        }

        private PlacementMode _currentPlacement = PlacementMode.Left;
        public PlacementMode CurrentPlacement
        {
            get => _currentPlacement;
            set => SetProperty(ref _currentPlacement, value);
        }
        /*--Команды---------------------------------------------------------------------------------------*/
        
        public DelegateCommand<UIElement>? ShowCommand { get; private init; }

        private void Show(UIElement target)
        {
            CurrentPlacement = PlacementMode.Left;
            PlacementTarget = target;
            IsOpen = true;
        }

        public DelegateCommand? ShowAtMouseCommand { get; private init; }

        public void ShowAtMouse()
        {
            CurrentPlacement = PlacementMode.MousePoint;
            PlacementTarget = null;
            IsOpen = true;
        }

        private bool CanShow(UIElement? target = null)
        {
            if (_condition is null) return true;
            return _condition();
        }

        public DelegateCommand? HideCommand { get; private init; }

        private void Hide()
        {
            IsOpen = false;
            PlacementTarget = null;
        }

        /*--Дополнительная логика-------------------------------------------------------------------------*/

        private double _height = double.NaN;
        public double Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }

        private double _maxHeight = double.PositiveInfinity;
        public double MaxHeight
        {
            get => _maxHeight;
            set => SetProperty(ref _maxHeight, value);
        }

        public CustomPopupPlacementCallback? CustomPlacementCallback { get; private set; }

        public void UpdatePopupSize(double containerActualHeight, double contentActualHeight = 0)
        {
            if (containerActualHeight > 100)
                MaxHeight = containerActualHeight - 100;

            if (contentActualHeight > 0)
                Height = contentActualHeight + 100;
        }

        private CustomPopupPlacement[] PlacePopupBottomCenter(Size popupSize, Size targetSize, Point offset)
        {
            double x = (targetSize.Width - popupSize.Width) / 2;
            double y = targetSize.Height;
            return
            [
                new CustomPopupPlacement(new Point(x, y), PopupPrimaryAxis.Horizontal)
            ];
        }

        private CustomPopupPlacement[] PlacePopupRightUp(Size popupSize, Size targetSize, Point offset)
        {
            if (PlacementTarget is FrameworkElement target)
            {
                double xOffset = target.ActualWidth;
                double yOffset = 0;
                return [new CustomPopupPlacement(new Point(xOffset + 10, (yOffset - popupSize.Height) + 25), PopupPrimaryAxis.Vertical)];
            }
            return [new CustomPopupPlacement(new Point(0, 0), PopupPrimaryAxis.Vertical)];
        }
    }
}