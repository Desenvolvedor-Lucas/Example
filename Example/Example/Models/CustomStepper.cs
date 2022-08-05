using System;
using Xamarin.Forms;

namespace Example.Models
{
    public class CustomStepper : StackLayout
    {
        public Button PlusBtn;
        public Button MinusBtn;
        public Entry Entry;

        public static readonly BindableProperty TextProperty =
          BindableProperty.Create(
             propertyName: "Text",
              returnType: typeof(int),
              declaringType: typeof(CustomStepper),
              defaultValue: 1,
              defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty TextColorProperty =
          BindableProperty.Create(
             propertyName: "TextColor",
              returnType: typeof(Color),
              declaringType: typeof(CustomStepper),
              defaultValue: Color.Red,
              defaultBindingMode: BindingMode.TwoWay); 

        public int Text
        {
            get { return (int)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }


        public CustomStepper()
        {
            MinusBtn = new Button
            {
                Text = "-",
                TextColor = this.TextColor,
                WidthRequest = 40,
                FontAttributes = FontAttributes.Bold,
                CornerRadius = 100,
                FontSize = 15
            };
            PlusBtn = new Button
            {
                Text = "+",
                TextColor = this.TextColor,
                WidthRequest = 40,
                FontAttributes = FontAttributes.Bold,
                CornerRadius = 100,
                FontSize = 15
            };
            switch (Device.RuntimePlatform)
            {

                case Device.UWP:
                case Device.Android:
                    {
                        MinusBtn.BackgroundColor = Color.LightGray;
                        PlusBtn.BackgroundColor = Color.LightGray;
                        break;
                    }
                case Device.iOS:
                    {
                        MinusBtn.BackgroundColor = Color.DarkGray;
                        PlusBtn.BackgroundColor = Color.DarkGray;
                        break;
                    }
            }

            Orientation = StackOrientation.Horizontal;
            PlusBtn.Clicked += PlusBtn_Clicked;
            MinusBtn.Clicked += MinusBtn_Clicked;
            Entry = new Entry
            {
                PlaceholderColor = Color.Gray,
                HorizontalTextAlignment = TextAlignment.Center,
                Keyboard = Keyboard.Numeric,
                WidthRequest = 40,
                BackgroundColor = Color.Transparent
            };

            Entry.SetBinding(Entry.TextProperty, new Binding(nameof(Text), BindingMode.TwoWay, source: this));
            Entry.SetBinding(Entry.TextColorProperty, new Binding(nameof(TextColor), BindingMode.TwoWay, source: this));
            Entry.TextChanged += Entry_TextChanged;

            MinusBtn.SetBinding(Entry.TextColorProperty, new Binding(nameof(TextColor), BindingMode.TwoWay, source: this));
            PlusBtn.SetBinding(Entry.TextColorProperty, new Binding(nameof(TextColor), BindingMode.TwoWay, source: this));

            Children.Add(MinusBtn);
            Children.Add(Entry);
            Children.Add(PlusBtn);
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
                this.Text = int.Parse(e.NewTextValue);
        }

        private void MinusBtn_Clicked(object sender, EventArgs e)
        {
            if (Text > 1)
                Text--;
        }

        private void PlusBtn_Clicked(object sender, EventArgs e)
        {
            if (Text < 10)
                Text++;
        }
    }
}
