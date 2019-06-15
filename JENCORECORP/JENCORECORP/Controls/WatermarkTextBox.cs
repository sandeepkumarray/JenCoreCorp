using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace JENCORECORP
{
    public class WatermarkTextBox : System.Windows.Controls.TextBox
    {
        public static readonly DependencyProperty SelectAllOnGotFocusProperty = DependencyProperty.Register("SelectAllOnGotFocus", typeof(bool), typeof(WatermarkTextBox), new PropertyMetadata((object)false));
        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register("Watermark", typeof(object), typeof(WatermarkTextBox), (PropertyMetadata)new UIPropertyMetadata((PropertyChangedCallback)null));
        public static readonly DependencyProperty WatermarkTemplateProperty = DependencyProperty.Register("WatermarkTemplate", typeof(DataTemplate), typeof(WatermarkTextBox), (PropertyMetadata)new UIPropertyMetadata((PropertyChangedCallback)null));

        public bool SelectAllOnGotFocus
        {
            get
            {
                return (bool)this.GetValue(WatermarkTextBox.SelectAllOnGotFocusProperty);
            }
            set
            {
                this.SetValue(WatermarkTextBox.SelectAllOnGotFocusProperty, (object)(bool)(value ? true : false));
            }
        }

        public object Watermark
        {
            get
            {
                return this.GetValue(WatermarkTextBox.WatermarkProperty);
            }
            set
            {
                this.SetValue(WatermarkTextBox.WatermarkProperty, value);
            }
        }

        public DataTemplate WatermarkTemplate
        {
            get
            {
                return (DataTemplate)this.GetValue(WatermarkTextBox.WatermarkTemplateProperty);
            }
            set
            {
                this.SetValue(WatermarkTextBox.WatermarkTemplateProperty, (object)(DataTemplate)value);
            }
        }

        static WatermarkTextBox()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(WatermarkTextBox), (PropertyMetadata)new FrameworkPropertyMetadata((object)typeof(WatermarkTextBox)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            if (!this.SelectAllOnGotFocus)
                return;
            this.SelectAll();
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (!this.IsKeyboardFocused && this.SelectAllOnGotFocus)
            {
                e.Handled = true;
                this.Focus();
            }
            base.OnPreviewMouseLeftButtonDown(e);
        }
    }
}
