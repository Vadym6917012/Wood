using Wood.Domain.Entities;

namespace Wood.Infrastructure.Persistence
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext db)
        {
            if ( db.Categories.Any() ) return;

            var categories = new List<Category>
        {
            new() { Id=1, Name="Дошки обрізні",    Slug="obrizni",   Icon="🪵" },
            new() { Id=2, Name="Дошки необрізні",  Slug="neobrizni", Icon="🌲" },
            new() { Id=3, Name="Брус та балки",    Slug="brus",      Icon="🔩" },
            new() { Id=4, Name="Вагонка",          Slug="vagonka",   Icon="🏠" },
            new() { Id=5, Name="Паркет та підлога",Slug="parket",    Icon="✨" },
        };

            db.Categories.AddRange(categories);
            db.SaveChanges();

            var products = new List<Product>
        {
            new() { Id=1,  Name="Дошка обрізна сосна 25×100×6000", Species="Сосна", Grade="1 сорт",
                Dimensions="25×100×6000 мм", PricePerCubicMeter=7200, PricePerPiece=108,
                Unit="м³", CategoryId=1, InStock=true, IsFeatured=true,
                Description="Суха обрізна дошка з сосни першого сорту. Підходить для опалубки, перекриттів та внутрішніх робіт.", ImageUrl="" },

            new() { Id=2,  Name="Дошка обрізна сосна 25×150×6000", Species="Сосна", Grade="1 сорт",
                Dimensions="25×150×6000 мм", PricePerCubicMeter=7200, PricePerPiece=162,
                Unit="м³", CategoryId=1, InStock=true, IsFeatured=true,
                Description="Широка обрізна дошка з сосни. Підходить для настилів, перегородок та оздоблення.", ImageUrl="" },

            new() { Id=3,  Name="Дошка обрізна дуб 32×120×4000", Species="Дуб", Grade="1 сорт",
                Dimensions="32×120×4000 мм", PricePerCubicMeter=18500, PricePerPiece=284,
                Unit="м³", CategoryId=1, InStock=true, IsFeatured=true,
                Description="Преміальна дубова дошка для меблів, підлог та дизайнерських інтер'єрів.", ImageUrl="" },

            new() { Id=4,  Name="Дошка обрізна ялина 25×100×6000", Species="Ялина", Grade="2 сорт",
                Dimensions="25×100×6000 мм", PricePerCubicMeter=6800, PricePerPiece=102,
                Unit="м³", CategoryId=1, InStock=true,
                Description="Економ-варіант із ялини для будівельних і опоряджувальних робіт.", ImageUrl="" },

            new() { Id=5,  Name="Дошка необрізна сосна 25мм", Species="Сосна", Grade="—",
                Dimensions="25 мм, довжина 4-6 м", PricePerCubicMeter=4500, PricePerPiece=0,
                Unit="м³", CategoryId=2, InStock=true,
                Description="Сирова необрізна дошка. Підходить для огорож, тимчасових будівель, обшивки.", ImageUrl="" },

            new() { Id=6,  Name="Дошка необрізна дуб 40мм", Species="Дуб", Grade="—",
                Dimensions="40 мм, довжина 2-5 м", PricePerCubicMeter=9800, PricePerPiece=0,
                Unit="м³", CategoryId=2, InStock=false,
                Description="Масивна необрізна дубова дошка для столярних і меблевих виробів.", ImageUrl="" },

            new() { Id=7,  Name="Брус сосна 100×100×6000", Species="Сосна", Grade="1 сорт",
                Dimensions="100×100×6000 мм", PricePerCubicMeter=8200, PricePerPiece=492,
                Unit="м³", CategoryId=3, InStock=true, IsFeatured=true,
                Description="Будівельний брус із сосни для каркасів, ферм та несучих конструкцій.", ImageUrl="" },

            new() { Id=8,  Name="Брус сосна 150×150×6000", Species="Сосна", Grade="1 сорт",
                Dimensions="150×150×6000 мм", PricePerCubicMeter=8200, PricePerPiece=1107,
                Unit="м³", CategoryId=3, InStock=true,
                Description="Великогабаритний брус для лагів, колон та несучих стін.", ImageUrl="" },

            new() { Id=9,  Name="Балка дерев'яна 50×200×6000", Species="Сосна", Grade="1 сорт",
                Dimensions="50×200×6000 мм", PricePerCubicMeter=8500, PricePerPiece=510,
                Unit="м³", CategoryId=3, InStock=true,
                Description="Балка для перекриттів та покрівельних систем.", ImageUrl="" },

            new() { Id=10, Name="Вагонка сосна 12×96×2000 Євро", Species="Сосна", Grade="А",
                Dimensions="12×96×2000 мм", PricePerCubicMeter=0, PricePerPiece=45,
                Unit="пог.м", CategoryId=4, InStock=true, IsFeatured=true,
                Description="Євровагонка сосна класу А для лазень, саун та оздоблення фасадів.", ImageUrl="" },

            new() { Id=11, Name="Вагонка липа 12×96×2000", Species="Липа", Grade="А",
                Dimensions="12×96×2000 мм", PricePerCubicMeter=0, PricePerPiece=95,
                Unit="пог.м", CategoryId=4, InStock=true,
                Description="Вагонка з липи — ідеальна для сауни та лазні. Не нагрівається, приємний аромат.", ImageUrl="" },

            new() { Id=12, Name="Паркетна дошка дуб 18×120×2200", Species="Дуб", Grade="Преміум",
                Dimensions="18×120×2200 мм", PricePerCubicMeter=0, PricePerPiece=420,
                Unit="м²", CategoryId=5, InStock=true, IsFeatured=true,
                Description="Масивна паркетна дошка з дуба. Покрита олією. Довговічне та благородне покриття.", ImageUrl="" },
        };

            db.Products.AddRange(products);
            db.SaveChanges();
        }

    }
}
