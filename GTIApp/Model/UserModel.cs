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
        public string FullName { get; set; }
        public int Age { get; set; }
    }
}
