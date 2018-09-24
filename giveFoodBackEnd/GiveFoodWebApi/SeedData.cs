using GiveFoodData;
using GiveFoodDataModels;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GiveFoodWebApi
{
    public class UserData
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public UserType Type { get; set; }
        public UserStatus Status { get; set; }
        public string Password { get; set; }

    }
    public static class SeedData
    {
        public static List<UserData> Users = new List<UserData>
        {
            new UserData
            {
                Description = "Като международна търговска компания ние се фокусираме върху резултатите, динамиката и коректното отношение. Това са ценностите, които ни мотивират. На тях се базира и нашият модел на управление. Така ние правим промените възможни и постигаме нашите цели. Научете повече и открийте това, което ни мотивира.",
                Name = "Kaufland",
                Email = "kaufland@aaa.com",
                Type =  UserType.Giver,
                Status = UserStatus.Approved,
                Password = "Admin098@",
            },
             new UserData
            {
                Description = @"Лидер в Европа. Четвърти в света. Водеща в България. Така изглежда марката Lidl в търговията с бързооборотни стоки.
Компанията оперира в над 27 държави с над 10 000 магазина и повече от 110 регионални центрове за дистрибуция в Европа. През 2017 година веригата прави следващата важна крачка в своята експанзия, като стъпва на американския пазар.
Историята на успеха на Лидл България започва на 25 ноември 2010 година, когато в рамките на един ден са открити първите 14 магазина в 11 града в страната. В началото на 2017 година компанията има 86 магазина в 45 града и продължава да планира смело, инвестирайки в нови обекти и откриването на втори логистичен център в с. Кабиле, област Ямбол.",
                Name = "Lidl",
                Email = "lidl@aaa.com",
                Type =  UserType.Giver,
                Status = UserStatus.Approved,
                Password = "Admin098@",
            },
             new UserData
            {
                Description = @"Happy Bar & Grill е верига от ежедневни ресторанти в България и Барселона. Благодарение на изключително доброто качество на храната и високия стандарт на обслужване, в България марката Happy Bar & Grill е най-силната в класа си, а също и най-многобройната. Заведенията ни са разположени на най-централните места в големите градове, по магистралите и главните пътища на страната. Освен бранда Happy, нашата Компания основава и развива редица други марки: ",
                Name = "Happy",
                Email = "office@happy.bg",
                Type =  UserType.Giver,
                Status = UserStatus.Approved,
                Password = "Admin098@",
            },
            new UserData
            {
                Description = @"Happy Bar & Grill е верига от ежедневни ресторанти в България и Барселона. Благодарение на изключително доброто качество на храната и високия стандарт на обслужване, в България марката Happy Bar & Grill е най-силната в класа си, а също и най-многобройната. Заведенията ни са разположени на най-централните места в големите градове, по магистралите и главните пътища на страната. Освен бранда Happy, нашата Компания основава и развива редица други марки: ",
                Name = "Happy",
                Email = "office@happy.bg",
                Type =  UserType.Giver,
                Status = UserStatus.Approved,
                Password = "Admin098@",
            },
             new UserData
            {
                Description = @"TimeHeroes е платформа за доброволчество и правене на добро. На повече добро.Тук ще намериш идеи как да превърнеш времето и уменията си в суперпозитивна сила. Защото си герой. Даже и да не го знаеш още.",
                Name = "TimeHeroes",
                Email = "timeheroes@aaa.bg",
                Type =  UserType.Taker,
                Status = UserStatus.Approved,
                Password = "Admin098@",
            },
            new UserData
            {
                Description = @"Надежда България е сайт за благотворителност и безвъзмездна помощ, който има за цел да подпомогне с информация и да свързва хората, които искат да помагат, с тези, които имат нужда от помощ. Ако търсите помощ, регистрирайте се в нашата Фейсбук група Надежда България и публикувайте съобщение, като опишете вашия проблем и каква точно помощ търсите.
Ако искате да помогнете, прегледайте съобщенията в групата Надежда България и участвайте.Ето какво може да видите на някои от популярните страници на сайта:
Домове за сираци - списък с домовете за деца в България, лишени от родителска грижа
Домове за стари хора - държавни домове за стари хора в България
Как да дарим или да помогнем - съвети към хората, желаещи да помагат или даряват",
                Name = "Надежда България",
                Email = "nadejdabulgaria@aaa.bg",
                Type =  UserType.Taker,
                Status = UserStatus.Approved,
                Password = "Admin098@",
            },
             new UserData
            {
                Description = @"Сайтът milostiv.org е вдъхновен от живота и дейността на св. Филарет Милостиви и стартира на 01 Декември 2011, когато ние - православните християни честваме неговата памет. С голямата помощ на множество доброволци и дарители успяхме да съберем информация за домове за деца и възрастни хора, както и да осъществим няколко други инициативи като например да организираме конкурси в различни области, да заснемем краткотраен документален филм, да съдействаме за даряването на над 100 компютъра на домове за деца и др. Водени от желанието да подпомагаме осъществяването на връзката между нуждаещи се хора и тези, които желаят и имат възможност да помогнат, публикуваме редовно предварително проверени призиви. За тях предоставяме контакти и банкови сметки директно на хората, които са в нужда. Стремим се да вдъхновим добрите дела и затова публикуваме на сайта новини и статии за благородни хора, които жертват от своето и от себе си, за да помогнат на ближните си. Наши инциативи са били подкрепяни от медии като БНТ, БНР, Дарик радио, Сайта на БПЦ и множество други сайтове. Пет години организацията беше неформална, което създаваше някои затруднения и ограничения при работа с администрацията и осъществяването на инициативите. По тази причина, в началото на 2017-а година, заедно с още няколко активни съмишленици, доказали с делата си, че са отдадени на каузата да помагат, регистрирахме Сдружение Милостив. Два от основните принципа на новооткритата организация са доброволен труд и пълна прозрачност. Публикуваме банкови сметки на нуждаещите се, но в редки случаите, когато това е невъзможно или неудачно, използваме сметката на сдружението като всяка дарена стотинка отива по желание на дарителя и всеки може да проследи дарението от следния адрес - milostiv.org/donations.php.Основните цели, които заложихме в устава на сдружение Милостив са продължение на досегашната дейност, а именно:1. Да подпомага и съдейства за преодоляване на социалното отчуждение, насърчаване на социалната интеграция и личностната реализация на социално слаби хора, деца, лишени от родителски грижи, хора с увреждания и лица, нуждаещи се от социални грижи;
2. Да помага за събирането на средства за лечението и закупуването на лекарства и консумативи на хора със здравословни проблеми, които са материално затруднени;
3. Да подпомага творческото развитие и образование на талантливи младежи, занимаващи се с култура или наука;
4. Да насърчава и популяризира доброволчеството, благотворителността и жертвоготовността в името на нуждаещите се;
5. Да бъде в синхрон с православните ценности и морал и в това число да подпомага строежи и строителни ремонти на православни храмове.
Вярваме, че делата на милосърдие са важни не само за човека в нужда, но и за този, който дарява, а и за обществото, в което живеем. Надяваме се, че с помощта на множество съмишленици ще можем да допринесем поне малко за разпространението на добрите дела сред нас.",
                Name = "Милостив",
                Email = "milostiv@aaa.com",
                Type =  UserType.Taker,
                Status = UserStatus.Approved,
                Password = "Admin098@",
            },
              new UserData
            {
                Description = @"Доброволческата кухня за нуждаещи се жители на енорията при храм „Свети Пророк Илия” в жк „Дружба” 2, гр. София, е инициатива на група млади хора, черкуващи се в Храма. Началото беше поставено на 21.11.1999 г., като след неделната служба групата се събра и реши да започне подготовка. За целта се събираха дърва за горене, пари и продукти, с които да се осъществи целият процес по изхранване на нуждаещите се. По списъци на социално слаби от общината и бездомници около Храма се събраха 35 души, на които започна раздаване на храна. Благословение за инициативата бе дадено както от свещениците при Храма, така и от Траянополския епископ Иларион, който лично присъства на първото готвене на 09.01.2000. Постепенно броят на хората, получаващи топла храна веднъж седмично , за 17 години нарасна до 400 души. През цялото това време храна се е раздавала във всеки неделен ден без прекъсване. Дейността на кухнята се осъществява с помощта на доброволци, организирани за подготовка на продукти и приготвяне на храната. Екипът на Кухнята е в основата на новосформираното Братство при Храма и членовете му активно помагат в работата и на другите екипи. Постоянните участници са формирани в екипи от 3 души, които организират дежурства по график с помощта на много доброволци както от нашия град, така и от цял свят. Организирани групи идват от фирми, организации, университети и училища. Кухнята разполага с четири фризера за съхранение на обработени продукти, склад и покрито дворно помещение с две каминни огнища на дърва и мебелно оборудване за съхранение на продукти и консумативи. Към днешна дата капацитетът на кухнята е 420 порции храна. Средствата се събират както от членовете на екипа, така и от дарения. Въведоха се стандарти за хигиенни изисквания относно процесите на готвене и раздаване на храна, обработка и съхранение на продукти, както и за цялостното организиране на дейността на кухнята.
В последните години като редовни дарители и партньори се изявяват Хлебозавод „Елиаз”; Българска хранителна банка, Фондация „Комунитас”; „Еднократна Употреба” ООД и много частни лица.
От месец ноември 2017 г. готвенето и раздаването на топла храна от кухнята се осъществява всяка събота.
Всеки, който желае да се включи като доброволец или дарител в дейността на доброволческата кухня за нуждаещи се към Храма, е добре дошъл всяка събота от 8.30 часа.",
                Name = "Православен Храм Св. пророк Илия",
                Email = "hramilia@aaa.com",
                Type =  UserType.Taker,
                Status = UserStatus.Approved,
                Password = "Admin098@",
            }
        };

        public static async Task Initialize(IServiceProvider serviceProvider, string password)
        {
            using (var context = new GiveFoodDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<GiveFoodDbContext>>()))
            {
                var adminID = await EnsureUser(serviceProvider, password, "admin@admin.com", "Administrator", "Аз съм системния администратор. Аз одобрявам или отхвърлям кандидатурите", UserType.Admin, UserStatus.Approved);
                await EnsureRole(serviceProvider, adminID, UserType.Admin.ToString());
                foreach (var user in Users)
                {
                    var userCreated = await EnsureUser(serviceProvider, user.Password, user.Email, user.Name, user.Description, user.Type, user.Status);
                    if (user.Status == UserStatus.Approved)
                    {
                        await EnsureRole(serviceProvider, userCreated, user.Type.ToString());
                    }
                }                
            }
        }

        private static async Task<Guid> EnsureUser(
            IServiceProvider serviceProvider,
            string password,
            string email,
            string name,
            string description,
            UserType typeId,
            UserStatus status)
        {
            var userManager = serviceProvider.GetService<UserManager<User>>();

            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new User { UserName = email, Email = email, Type = typeId, Name = name, Description = description, Status = status };
                var result = await userManager.CreateAsync(user, password);
                result = await userManager.AddClaimsAsync(user, new Claim[]{
                        new Claim(JwtClaimTypes.Name, name),
                        new Claim(JwtClaimTypes.Email, email),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean)
                });
            }

            return user.Id;
        }
        private static async Task CreateRole(string role, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetService<RoleManager<UserRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new UserRole()
                {
                    Name = role,
                    Description = role,
                    CreatedDate = DateTime.UtcNow,
                    IPAddress = GetLocalIPAddress()
                });
            }
        }

        private static async Task<IdentityResult> EnsureRole(
            IServiceProvider serviceProvider,
            Guid uid,
            string role)
        {

            await CreateRole(role, serviceProvider);

            var userManager = serviceProvider.GetService<UserManager<User>>();

            var user = await userManager.FindByIdAsync(uid.ToString());

            var IR = await userManager.AddToRoleAsync(user, role);

            await userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Role, role));

            return IR;
        }


        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
