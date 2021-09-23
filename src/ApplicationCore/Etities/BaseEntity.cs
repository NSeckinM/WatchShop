namespace ApplicationCore.Etities
{
    //Bu class diğer bütün classlarda da Id entitisi olduğu için miras alan classların hepsinde kullanılabilmesi amacıyla abstract olarak oluşturuldu
    public abstract class BaseEntity    //<T> ıd propunun türünü generic yapmak istiyorsak int yerinede T demeliyiz
    {
        public virtual int Id { get; set; }


    }
}
