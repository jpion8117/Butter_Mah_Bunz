using System;
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

namespace Butter_Mah_Bunz
{
    /// <summary>
    /// Interaction logic for ItemDetailPage.xaml
    /// </summary>
    public partial class ItemDetailPage : Page
    {
        private Backend.Item _baseItem;
        private Backend.EnhancedItem _enhancedItem;
        public ItemDetailPage(string baseItemName)
        {
            InitializeComponent();

            //lookup base item
            Backend.Item? item = CoreComponents.locateMenuItem(baseItemName);
            _baseItem = new Backend.Item("", "", 0);
            if (item != null)
                _baseItem = new Backend.Item(item);

            _enhancedItem = new Backend.EnhancedItem(_baseItem);

            //add item image and details to ItemInfo


            //add available enhancments Enhancments

        }
    }
}
