using GTIApp.View;
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
                 /*new MenuModel { Id = 5, Name = "Home", Detail = "", Icon = "homemenu.png" },*/
                new MenuModel { Id = 1, Name = "Perfil", Detail = "", Icon = "contact.png" },
                new MenuModel { Id = 2, Name = "Nuevo contacto", Detail = "", Icon = "lupa.png" },
                new MenuModel { Id = 4, Name = "Mapa", Detail = "", Icon = "agregarcontacto50.png" },
                new MenuModel { Id = 3, Name = "Cerrar Sesión", Detail = "", Icon = "cerrarsesion100.png" },                
               /* new MenuModel { Id = 6, Name = "Carousel", Detail = "", Icon = "" },
                new MenuModel { Id = 7, Name = "Camara", Detail = "", Icon = "" },*/
            };
            
            return lstMenu;
        }

    }
}
