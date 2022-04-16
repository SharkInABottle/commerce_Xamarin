using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace commerce.Pages.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileTabbed : TabbedPage
    {
        public ProfileTabbed()
        {
            InitializeComponent();
        }
        public ProfileTabbed(string x)
        {
            InitializeComponent();
            if (x == "messages") this.CurrentPage=this.Children[2];
        }
    }
}