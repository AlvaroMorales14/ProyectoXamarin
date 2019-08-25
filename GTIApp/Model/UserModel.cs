using Realms;
using System;
namespace GTIApp.Model
{
    public class UserModel: RealmObject
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pass { get; set; }
    }
}
