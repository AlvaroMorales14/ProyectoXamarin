using System;
using System.Collections.ObjectModel;
using VectorIcon.FormsPlugin.Abstractions;

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
                new MenuModel { Id = 1, Name = "Mi perfil", Detail = "", Icon = "https://www.iconsdb.com/icons/preview/caribbean-blue/user-4-xxl.png" },
                new MenuModel { Id = 2, Name = "Agregar contacto", Detail = "", Icon = "https://publicdomainvectors.org/photos/hand-glass-stylized-mar-03r.png" },                
                new MenuModel { Id = 4, Name = "Mapa", Detail = "", Icon = "agregarcontacto50.png" },
                new MenuModel { Id = 3, Name = "Cerrar Sesión", Detail = "", Icon = "cerrarsesion100.png" },
            };
            
            return lstMenu;
        }        
    }
}
