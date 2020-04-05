﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TranslatorLibrary;

namespace MisakaTranslator_WPF.SettingsPages.TranslatorPages
{
    /// <summary>
    /// TencentOldTransSettingsPage.xaml 的交互逻辑
    /// </summary>
    public partial class TencentOldTransSettingsPage : Page
    {
        public TencentOldTransSettingsPage()
        {
            InitializeComponent();
            TransAppIDBox.Text = Common.appSettings.TXOSecretId;
            TransSecretKeyBox.Text = Common.appSettings.TXOSecretKey;
        }

        private void AuthTestBtn_Click(object sender, RoutedEventArgs e)
        {
            Common.appSettings.TXOSecretId = TransAppIDBox.Text;
            Common.appSettings.TXOSecretKey = TransSecretKeyBox.Text;
            ITranslator Trans = new TencentOldTranslator();
            Trans.TranslatorInit(TransAppIDBox.Text, TransSecretKeyBox.Text);
            if (Trans.Translate("apple", "zh", "en") != null)
            {
                HandyControl.Controls.Growl.Success("腾讯私人API工作正常!");
            }
            else
            {
                HandyControl.Controls.Growl.Error("腾讯私人API工作异常\n" + Trans.GetLastError());
            }
        }

        private void ApplyBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(TencentOldTranslator.GetUrl_allpyAPI());
        }

        private void DocBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(TencentOldTranslator.GetUrl_Doc());
        }

        private void BillBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(TencentOldTranslator.GetUrl_bill());
        }

        private void TransTestBtn_Click(object sender, RoutedEventArgs e)
        {
            ITranslator Trans = new TencentOldTranslator();
            Trans.TranslatorInit(Common.appSettings.TXOSecretId, Common.appSettings.TXOSecretKey);
            string res = Trans.Translate(TestSrcText.Text, TestDstLang.Text, TestSrcLang.Text);
            if (res != null)
            {
                HandyControl.Controls.MessageBox.Show(res, "翻译结果");
            }
            else
            {
                HandyControl.Controls.Growl.Error("腾讯私人翻译API工作异常\n" + Trans.GetLastError());
            }
        }
    }
}