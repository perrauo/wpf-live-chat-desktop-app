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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IFT585_TP3.Client
{  
    public partial class GroupRequestToastControl : BaseToastControl
    {
        static GroupRequestToastControl()
        {
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(GroupRequestToastControl), new FrameworkPropertyMetadata(typeof(GroupRequestToastControl)));
        }
    }

}