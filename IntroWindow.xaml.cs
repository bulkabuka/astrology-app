﻿using System.Windows;
using System.Windows.Media;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls.Window;

namespace AstrologyApp
{
    public partial class IntroWindow : FluentWindow
    {
        public IntroWindow()
        {
            InitializeComponent();
            FontFamily = new FontFamily("Segoe UI");
            Theme.Apply(ThemeType.Light, WindowBackdropType.Mica, false);
            var primaryAccent = Color.FromRgb(103, 80, 164);
            Accent.Apply(primaryAccent);
            var intro_text = @"Лицензионное соглашение.
Настоящее Лицензионное Соглашение представляет собой юридическое Соглашение (ст.1235,1286 Гражданского кодекса РФ) между физическим лицом (ПОЛЬЗОВАТЕЛЕМ) и группой разработчиков продуктов «Точка опоры» (ПРАВООБЛАДАТЕЛЬ), на ПРОГРАММНЫЙ ПРОДУКТ «Точка опоры. В личном.», поставляемый по настоящему Соглашению, включая компьютерное программное обеспечение, и все содержимое файлов, диска(-ов), компакт-диска(-ов) и иных средств, к которым прилагается настоящее Соглашение, а также сопровождающие печатные материалы и электронную документацию.

Устанавливая, копируя, загружая, получая доступ или иным образом используя ПРОГРАММНЫЙ ПРОДУКТ, ПОЛЬЗОВАТЕЛЬ соглашается придерживаться условий настоящего Соглашения, которое имеет приоритет над любым другим документом и регулирует использование ПОЛЬЗОВАТЕЛЕМ ПРОГРАММНОГО ПРОДУКТА.

В соответствии с условиями настоящего Соглашения ПРАВООБЛАДАТЕЛЬ предоставляет ПОЛЬЗОВАТЕЛЮ право на установку и использование одного экземпляра ПРОГРАММНОГО ПРОДУКТА, а также право на изготовление резервных копий, используемых в случае утраты или порчи основных носителей из состава ПРОГРАММНОГО ПРОДУКТА.

ПОЛЬЗОВАТЕЛЬ не вправе изменять, декомпилировать, дешифровать и производить иные действия с объектным кодом ПРОГРАММНОГО ПРОДУКТА, направленные на получение информации о реализации алгоритмов, используемых в ПРОГРАММНОМ ПРОДУКТЕ, а также создавать производные произведения с использованием ПРОГРАММНОГО ПРОДУКТА.

ПРАВООБЛАДАТЕЛЬ не несет ответственности за какие-либо косвенные убытки или иной ущерб (в том числе, без ограничения, опосредованные, особые или случайные убытки, убытки, в связи с упущенной прибылью, прерыванием коммерческой или производственной деятельности, утратой коммерческой информации, а также ущерб, нанесенный третьим лицам), возникающий в связи с использованием ПРОГРАММНОГО ПРОДУКТА, включая случаи ошибок и сбоев при функционировании ПРОГРАММНОГО ПРОДУКТА, даже в случае уведомления о возможности возникновения таких убытков или такого ущерба, или случаев, когда такая возможность была разумно предсказуема.

Простая (неисключительная) лицензия на ПРОГРАММНЫЙ ПРОДУКТ предоставляется на условиях «как есть»: ПРАВООБЛАДАТЕЛЬ не предоставляет никаких гарантий соответствия ПРОГРАММНОГО ПРОДУКТА конкретным целям и ожиданиям ПОЛЬЗОВАТЕЛЯ, а также не предоставляет никаких иных гарантий, прямо не указанных в настоящем Соглашении. 

ПОЛЬЗОВАТЕЛЬ принимает на себя ответственность за выбор ПРОГРАММНОГО ПРОДУКТА с целью достижения желаемых результатов и в отношении результатов, получаемых в ходе использования ПОЛЬЗОВАТЕЛЕМ ПРОГРАММНОГО ПРОДУКТА.

ПРАВООБЛАДАТЕЛЬ не несет ответственность за действия третьих лиц, технические сбои и перерывы в работе ПРОГРАММНОГО ПРОДУКТА, вызванные неполадками используемых технических средств, иные аналогичные сбои, а также вызванные неполадками компьютерного оборудования, которое ПОЛЬЗОВАТЕЛЬ использовал для работы с ПРОГРАММНЫМ ПРОДУКТОМ.

ПРАВООБЛАДАТЕЛЬ обязуется не распространять персональные данные и любые другие сведения о ПОЛЬЗОВАТЕЛЕ, ставшие известными в результате взаимодействия с ПОЛЬЗОВАТЕЛЕМ.

Правообладатель гарантирует функциональность и работоспособность ПРОГРАММНОГО ПРОДУКТА в соответствии с описанием, изложенным в документации, которые входят в состав ПРОГРАММНОГО ПРОДУКТА.";
            IntroText.Text = intro_text;
        }

        private void EnterBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var wind = new MainW();
            Close();
            wind.Show();
        }
    }
}