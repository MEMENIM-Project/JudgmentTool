using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgmentTool
{
    class PageNavigationManager
    {
        static public MetroContentControl PageContentControl
        {
            set { m_PageContentControl = value; }
        }

        static private MetroContentControl m_PageContentControl;

        public static void SwitchToPage(object page)
        {
            m_PageContentControl.Content = page;
        }

    }
}
