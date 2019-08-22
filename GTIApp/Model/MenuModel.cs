using System;
using System.Collections.ObjectModel;

namespace GTIApp.Model
{
    public class MenuModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Icon { get; set; }
       
        public static ObservableCollection<MenuModel> GetMenu()
        {
            ObservableCollection<MenuModel> lstMenu = new ObservableCollection<MenuModel>
            {
                new MenuModel { Id = 1, Name = "Mi Perfil", Detail = "", Icon = "https://www.iconsdb.com/icons/preview/caribbean-blue/user-4-xxl.png" },
                new MenuModel { Id = 2, Name = "Consultar", Detail = "", Icon = "https://publicdomainvectors.org/photos/hand-glass-stylized-mar-03r.png" },
                new MenuModel { Id = 3, Name = "Cerrar Sesión", Detail = "", Icon = "https://image.flaticon.com/icons/png/512/216/216937.png" },
                new MenuModel { Id = 4, Name = "Tabbed", Detail = "", Icon = "" }
            };
            
            return lstMenu;
        }        
    }
}
