using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

public class CatalogContext : DbContext
{
    public DbSet<CatalogBrand> CatalogBrands { get; set; }
    public DbSet<CatalogType> CatalogTypes { get; set; }
    public DbSet<CatalogItem> CatalogItems { get; set; }
    public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
    {
        try
        {
            var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            if (databaseCreator != null)
            {
                if (!databaseCreator.CanConnect()) databaseCreator.Create();
                if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CatalogBrand>().HasData(
            new CatalogBrand { Id = 1, Brand = "Apple" },
            new CatalogBrand { Id = 2, Brand = "Samsung" },
            new CatalogBrand { Id = 3, Brand = "Xiaomi" }
        );
        modelBuilder.Entity<CatalogType>().HasData(
            new CatalogType { Id = 1, Type = "Телефон" },
            new CatalogType { Id = 2, Type = "Смарт-часы" },
            new CatalogType { Id = 3, Type = "Наушники" }
        );
        modelBuilder.Entity<CatalogItem>().HasData(
            new CatalogItem { Id = 1, Name = "TWS Apple Airpods 3 белый", 
                Description = "Наушники TWS Apple Airpods 3 – легкая модель с эргономичными вкладышами, форма которых была оптимизирована для более плотной и комфортной посадки. Наилучшее звучание достигается за счет правильного расположения устройств в ушах, при этом обеспечивается хорошая изоляция слушателя от звуков окружающего мира. Ножка стала короче на 33% в сравнении с аналогичной моделью второго поколения, при этом в нее смог уместиться датчик нажатия, при помощи которого пользователь сможет управлять звонками и воспроизведением музыки.", 
                Price = 15499, CatalogBrandId = 1, CatalogTypeId = 3 },
            new CatalogItem { Id = 2, Name = "TWS Apple AirPods Pro белый",
                Description = "Наушники TWS Apple AirPods Pro отличает реализованная в них технология активного шумоподавления, благодаря которой отсекаются все звуки окружающего мира, что позволит с комфортом слушать музыку в самых шумных местах. Модель стала обладательницей внутриканальных амбушюр с силиконовыми вкладышами, которые представлены в комплекте поставки тремя парами разного размера.",
                Price = 17499, CatalogBrandId = 1, CatalogTypeId = 3 },
            new CatalogItem { Id = 3, Name = "TWS Apple AirPods Pro 2 белый",
                Description = "Наушники TWS Apple AirPods Pro 2 выполнены в фирменном стиле в корпусе белого цвета. Эргономичная конструкция с мягкими силиконовыми вкладышами и небольшим весом гарантирует максимально комфортное размещение наушников в слуховом канале. Синхронизация с устройством осуществляется посредством технологии Bluetooth при извлечении из кейса.",
                Price = 23999, CatalogBrandId = 1, CatalogTypeId = 3 },
            new CatalogItem { Id = 4, Name = "Apple AirPods Max серебристый",
                Description = "Bluetooth гарнитура Apple AirPods Max – модель полноразмерных наушников с элегантным исполнением, эргономичной конструкцией и характеристиками премиального уровня. Динамические драйверы формируют насыщенный реалистичный звук, погружающий в прослушивание музыки. Встроенный микрофон предоставляет возможность общения. Благодаря функции активного шумоподавления блокируются внешние шумы.",
                Price = 52999, CatalogBrandId = 1, CatalogTypeId = 3 },
            new CatalogItem { Id = 5, Name = "Apple AirPods Max синий",
                Description = "Bluetooth гарнитура Apple AirPods Max с охватывающими амбушюрами отличается богатством и высоким качеством звука. Они обладают особой конструкцией корпуса, что позволяет наушникам прилегать максимально плотно к голове разной формы, надежно удерживаться и не падать. Благодаря верхней сетчатой части оголовья веч устройства будет равномерно распределен по корпусу, за счет чего давление на голову будет уменьшено. Дужка выполнена из нержавеющей стали и покрыта мягким материалом для наибольшей гибкости, прочности и удобства оголовья. Для комфортного положения корпуса наушников на голове выдвигаются для надежной фиксации телескопические дужки.\r\nЧаши наушников Apple AirPods Max, выполненные из анодированного алюминия, содержат механизм, позволяющий каждому из них независимо поворачиваться для оптимального распределения давления. ",
                Price = 62999, CatalogBrandId = 1, CatalogTypeId = 3 }
        );
    } 
}
