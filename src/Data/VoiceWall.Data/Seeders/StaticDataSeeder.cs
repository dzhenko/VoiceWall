namespace VoiceWall.Data.Seeders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using VoiceWall.Common;
    using VoiceWall.Data.Models;

    internal static class StaticDataSeeder
    {
        private static readonly Random random = new Random();

        internal static void SeedUsers(VoiceWallDbContext context)
        {
            var names = GetUserNames();

            var profilePictures = GetProfileImages();

            var userManager = new UserManager<User>(new UserStore<User>(context));

            for (int i = 0; i < Math.Min(names.Length, profilePictures.Length); i++)
            {
                var user = new User()
                { 
                    UserName = string.Format("FakeUser{0}", i + 1),
                    Email = string.Format("FakeUser{0}@FakeEmail.com", i + 1),
                    FirstName = names[i].Substring(0, names[i].IndexOf(" ")),
                    LastName = names[i].Substring(names[i].IndexOf(" ") + 1),
                    UserImage = profilePictures[i]
                };

                userManager.Create(user, "qwerty");

                userManager.AddToRole(user.Id, GlobalConstants.DefaultRole);

                context.SaveChanges();
            }
        }

        internal static void SeedData(VoiceWallDbContext context)
        {
            var userIds = context.Users.Select(u => u.Id).ToArray();

            for (int i = 0; i < 50; i++)
            {
                var t = random.Next(0, 100) > 50 ? ContentType.Picture : random.Next(0, 100) > 50 ? ContentType.Sound : ContentType.Video;
                var u = t == ContentType.Picture ? GetImageUrl() : t == ContentType.Sound ? GetSoundUrl() : GetVideoUrl();

                context.Contents.Add(new Content() 
                {
                    ContentType = t,
                    ContentUrl = u,
                    UserId = userIds[random.Next(0, userIds.Length)],
                    ContentViews = new List<ContentView>()
                    {
                        new ContentView() { Liked = true, UserId = userIds[random.Next(0, userIds.Length)] },
                        new ContentView() { Liked = true, UserId = userIds[random.Next(0, userIds.Length)] },
                        new ContentView() { Liked = true, UserId = userIds[random.Next(0, userIds.Length)] },
                        new ContentView() { Liked = false, UserId = userIds[random.Next(0, userIds.Length)] },
                        new ContentView() { Liked = null, UserId = userIds[random.Next(0, userIds.Length)] },
                    },
                    Comments = GetComments(userIds)
                });

                context.SaveChanges();
            }
        }

        internal static void SeedAdmin(VoiceWallDbContext context)
        {
            const string AdminEmail = "qwe@qwe.com";
            const string AdminPassword = "qweqwe";

            if (context.Users.Any(u => u.Email == AdminEmail))
            {
                return;
            }

            var userManager = new UserManager<User>(new UserStore<User>(context));

            var admin = new User
            {
                FirstName = "Pesho",
                LastName = "Admina",
                Email = AdminEmail,
                UserName = AdminEmail,
                UserImage= GlobalConstants.DefaultUserPicture
            };

            userManager.Create(admin, AdminPassword);
            userManager.AddToRole(admin.Id, GlobalConstants.AdminRole);
            userManager.AddToRole(admin.Id, GlobalConstants.ModeratorRole);
            userManager.AddToRole(admin.Id, GlobalConstants.DefaultRole);

            context.SaveChanges();
        }

        internal static void SeedModerator(VoiceWallDbContext context)
        {
            const string ModeratorEmail = "moderator@moderator.com";
            const string ModeratorPassword = "moderator123456";

            if (context.Users.Any(u => u.Email == ModeratorEmail))
            {
                return;
            }

            var userManager = new UserManager<User>(new UserStore<User>(context));

            var admin = new User
            {
                FirstName = "Gosho",
                LastName = "Moderatora",
                Email = ModeratorEmail,
                UserName = ModeratorEmail,
                UserImage = GlobalConstants.DefaultUserPicture
            };

            userManager.Create(admin, ModeratorPassword);

            userManager.AddToRole(admin.Id, GlobalConstants.ModeratorRole);
            userManager.AddToRole(admin.Id, GlobalConstants.DefaultRole);

            context.SaveChanges();
        }

        internal static void SeedRoles(VoiceWallDbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            roleManager.Create(new IdentityRole { Name = GlobalConstants.DefaultRole });
            roleManager.Create(new IdentityRole { Name = GlobalConstants.AdminRole });
            roleManager.Create(new IdentityRole { Name = GlobalConstants.ModeratorRole });

            context.SaveChanges();
        }

        internal static void SeedJokes(VoiceWallDbContext context)
        {
            if (context.Jokes.Any())
            {
                return;
            }

            var allJokes = GetAllJokes();

            foreach (var joke in allJokes)
            {
                context.Jokes.Add(new Joke() { Text = joke });
            }

            context.SaveChanges();
        }

        private static ICollection<Comment> GetComments(string[] userIds)
        {
            var comments = new List<Comment>();

            for (int i = 0; i < 15; i++)
            {
                var ct = random.Next(0, 100) > 50 ? ContentType.Picture : random.Next(0, 100) > 50 ? ContentType.Sound : ContentType.Video;
                var cu = ct == ContentType.Picture ? GetImageUrl() : ct == ContentType.Sound ? GetSoundUrl() : GetVideoUrl();

                comments.Add(new Comment()
                {
                    ContentType = ct,
                    ContentUrl = cu,
                    UserId = userIds[random.Next(0, userIds.Length)],
                    CommentViews = new List<CommentView>()
                    {
                        new CommentView() {UserId = userIds[random.Next(0, userIds.Length)], Flagged = random.Next(0, 100) > 80},
                        new CommentView() {UserId = userIds[random.Next(0, userIds.Length)], Flagged = random.Next(0, 100) > 80},
                        new CommentView() {UserId = userIds[random.Next(0, userIds.Length)], Flagged = random.Next(0, 100) > 80},
                    }
                });
            }

            return comments;
        }

        private static string[] GetUserNames()
        {
            return new[] 
            {
                "Polly Dimitrova",
                "Petur Toshev",
                "Aleksii Todorov",
                "Dimitur Stoyanov",
                "Anna Georgieva",
                "Viktor Ivanov",
                "Vicktoria Petrova",
                "Sara Merkenzel",
                "Gosho Petrov",
                "Pesho Georgiev",
                "Ivan Klisurski",
                "Matt Deamon",
                "Peter Stoyanov",
                "Dragomir Petrov",
                "Dimitur Trifonov"
            };
        }

        private static string[] GetProfileImages()
        {
            return new[] 
            {
                "http://media.cirrusmedia.com.au/LW_Media_Library/594partner-profile-pic-An.jpg",
                "http://organicthemes.com/demo/profile/files/2012/12/profile_img.png",
                "https://lh5.googleusercontent.com/-ZadaXoUTBfs/AAAAAAAAAAI/AAAAAAAAAAA/3rh5IMTHOzg/photo.jpg",
                "http://www.binarytradingforum.com/core/image.php?userid=27&dateline=1355305878",
                "http://devilsworkshop.org/files/2013/01/enlarged-facebook-profile-picture.jpg",
                "http://media.cirrusmedia.com.au/LW_Media_Library/LW-603-p28-partner-profile.jpg",
                "http://2.bp.blogspot.com/-dZKdgsUW2y0/Une2h3IIVMI/AAAAAAAAC1o/tqJJFHKzHfY/s1600/katrina-kaif-Complete-Profile.jpg",
                "http://www.american.edu/uploads/profiles/large/SarahMenkeFish_profile.jpg",
                "http://blogs.cuit.columbia.edu/asj2122/files/2013/07/profile.jpg",
                "http://www.beatpennystocks.com/wp-content/uploads/2013/06/profile_face_small_normal.jpg",
                "https://lh6.googleusercontent.com/-epxHyUrTK90/AAAAAAAAAAI/AAAAAAAAABU/Q_07RVcKHPM/photo.jpg",
                "http://static.squarespace.com/static/50d68cabe4b02a03223818eb/t/51522834e4b05ee3451307f4/1364338741441/profile-matt-d.png",
                "http://johnjournal.bravesites.com/files/images/Profile_Score_Photo.jpg",
                "http://media.cirrusmedia.com.au/LW_Media_Library/LW-598-PARTNER-PROFILE-PIC.jpg",
                "http://www.american.edu/uploads/profiles/large/chris_palmer_profile_11.jpg"
            };
        }

        private static DateTime GetDate()
        {
            var date = DateTime.Now;
            date.AddDays((-1) * random.Next(0, 30));
            date.AddHours((-1) * random.Next(0, 23));
            date.AddMinutes((-1) * random.Next(0, 60));
            return date;
        }

        private static string GetSoundUrl()
        {
            var sounds = new[]
            {
                "http://www.music.helsinki.fi/tmt/opetus/uusmedia/esim/a2002011001-e02.wav",
                "http://www.music.helsinki.fi/tmt/opetus/uusmedia/esim/a2002011001-e02-16kHz.wav",
                "http://www.music.helsinki.fi/tmt/opetus/uusmedia/esim/a2002011001-e02-8kHz.wav",
                "http://www.music.helsinki.fi/tmt/opetus/uusmedia/esim/a2002011001-e02-ulaw.wav",
                "http://www.music.helsinki.fi/tmt/opetus/uusmedia/esim/a2002011001-e02-128k.mp3"
            };

            return sounds[random.Next(0, sounds.Length)];
        }

        private static string GetVideoUrl()
        {
            var videos = new[]
            {
                "http://techslides.com/demos/sample-videos/small.webm",
                "http://video.webmfiles.org/big-buck-bunny_trailer.webm",
                "http://video.webmfiles.org/elephants-dream.webm",
                "http://easyhtml5video.com/images/happyfit2.webm"
            };

            return videos[random.Next(0, videos.Length)];
        }

        private static string GetImageUrl()
        {
            var images = new[]
            {
                "http://cdn.buzznet.com/assets/users16/brittanyhagerty/default/sunday-best-breathtaking-sunsets-across--large-msg-13675373613.jpg",
                "http://images.fineartamerica.com/images-medium/breathtaking-sunset-larry-roby.jpg",
                "http://zuzutop.com/wp-content/uploads/2010/02/Breathtaking-Photographs-of-Nature-19.jpg",
                "http://i.telegraph.co.uk/multimedia/archive/02072/peak9_2072162i.jpg",
                "http://cl.jroo.me/z3/o/l/L/d/a.aaa-breathtaking-view.jpg",
                "http://www.englishforum.ch/attachments/travel-day-trips-free-time/19089d1285090388-lake-geneva-most-magnificent-breathtaking-lake-world-ab009945.jpg",
                "http://www.92pixels.com/wp-content/uploads/2012/10/Breathtaking-Photography18.jpg",
                "http://th09.deviantart.net/fs71/PRE/i/2010/262/0/d/breathtaking_view_by_uniquecreativity-d2yzabl.jpg",
                "http://api.ning.com/files/kV4MbYiv7oQtPJChuRhjk7eEQzxj03hl3hVs5hhDW50t3GI3VDERRV6c7e2ZIyB5GhMOJ-lx3tdSV66fa2Kn7UbRUswv2aEF/1082025894.jpeg",
                "http://www.crazyleafdesign.com/blog/wp-content/uploads/2009/12/breathtaking-nature-photos-3.jpg",
                "http://zuzutop.com/wp-content/uploads/2010/02/Breathtaking-Photographs-of-Nature-6.jpg",
                "http://aboutkazakhstan.com/blog/wp-content/uploads/2011/07/breathtaking-views-of-kazakhstan-nature-2.jpg",
                "http://www.mauiwine.com/userfiles/image/pages/THE_EXPERIENCE_RJB_021.1.jpg",
                "http://smashinghub.com/wp-content/uploads/2010/07/wave8.jpg",
                "http://webtoolfeed.files.wordpress.com/2012/08/satorini-pool-and-ocean-at-sunset-620x412.jpg",
                "http://amolife.com/image/images/stories/Nature/Flowers/breathtaking_macro_shots_1.jpg",
                "http://photoity.com/wp-content/uploads/2012/11/Breathtaking-Photography-by-Elizabeth-Gadd-2.jpg",
                "http://abduzeedo.com/files/originals/p/portraits_007_0.jpg",
                "http://photorator.com/photos/images/breathtaking-view-of-mount-fuji-japan--28976.jpg"
            };

            return images[random.Next(0, images.Length)];
        }

        private static IEnumerable<string> GetAllJokes()
        {
            return new List<string>() 
            {
@"Q: Why do Jews have long noses? A: Because air is free.",
@"Light travels faster than sound. This is why some people appear bright until you hear them speak.",
@"If a man opens the car door for his wife, you can be sure of one thing: either the car is new or the wife.",
@"Q: Why can't a blonde dial 911?A: She can't find the eleven.",
@"Helium walks into a bar and asks for a drink. The bartender says, ""Sorry, we don't serve noble gases here."" Helium doesn't react.",
@"Yo mama is so stupid she came over to my house and shouted in my mailbox to leave me voicemail.",
@"Why are asprins white? Because they work!",
@"A husband and wife are trying to set up a new password for their computer. The husband puts, ""Mypenis,"" and the wife falls on the ground laughing because on the screen it says, ""Error. Not long enough.""",
@"Q: Did you hear about the guy who drank 8 Cokes? A: He burped 7Up.",
@"Q: What did the duck say when he bought lipstick?A: ""Put it on my bill.""",
@"Do not be racist , be like Mario.  He's an italian plumber, made by Japanese people, who speaks english, looks like a mexican, jumps like a black man, and grabs coins like a jew!",
@"If at first you don't succeed, skydiving is not for you!",
@"Q: What is the difference between snowmen and snowwomen? A: Snowballs.",
@"What happens to a frog's car when it breaks down?It gets toad away.",
@"Blonde: ""What does IDK stand for?""",
@"Brunette: ""I dont know.""",
@"Blonde: ""OMG, nobody does!""",
@"Yo mamma is so ugly when she tried to join an ugly contest they said, ""Sorry, no professionals.""",
@"Wife: I look fat.  Can you give me a compliment? Husband: You have perfect eyesight.",
@"A husband and wife are trying to set up a new password for their computer. The husband puts, ""Mypenis,"" and the wife falls on the ground laughing because on the screen it says, ""Error. Not long enough.""",
@"Q: Why did they have to bury George Washington standing up? A: Because he could never lie.",
@"Yo momma is so fat she uses a pillow for a tampon.",
@"Yo momma is so fat, when she sat on an iPod, she made the iPad!",
@"The teacher asked Jimmy, ""Why is your cat at school today Jimmy?"" Jimmy replied crying, ""Because I heard my daddy tell my mommy, 'I am going to eat that p*ssy once Jimmy leaves for school today!'""",
@"Q: Which two letters in the alphabet are always jealous? A: NV.",
@"Yo momma is so fat when she got on the scale it said, ""I need your weight not your phone number.""",
@"Q: Why did President Obama get two terms? A: Because every black man gets a longer sentence.",
@"Q: Why do centipedes have 100 legs? A: So they can walk.",
@"Scientists have proven that there are two things in the air that have been known to cause women to get pregnant: their legs.",
@"A little kids sends a letter to Santa that says: ""Dear Santa I want a brother for Christmas.""",
@"Santa writes back, ""Dear Timmy send me me your mommy.""",
@"I named my hard drive ""dat ass,"" so once a month my computer asks if I want to ""back dat ass up.""",
@"Teacher: ""Kids,what does the chicken give you?""Student: ""Meat!""Teacher: ""Very good! Now what does the pig give you?""Student: ""Bacon!""Teacher: ""Great! And what does the fat cow give you?""Student: ""Homework!""",
@"Q. What did the elephant say to the naked man? A. ""How do you breathe through something so small?",
@"Teacher: ""Kids,what does the chicken give you?""Student: ""Meat!""Teacher: ""Very good! Now what does the pig give you?""Student: ""Bacon!""Teacher: ""Great! And what does the fat cow give you?""Student: ""Homework!""",
@"I don't really like watching basketball, I just watch it to find out who the next member of the Kardashian family will be.",
@"Mexico doesn't win Olympic medals because all the best runners, jumpers, and swimmers are in America.",
@"Q: What did the duck say when he bought lipstick?A: ""Put it on my bill.""",
@"How do you starve a black person? Put their food stamp card under their workboots!",
@"Blonde: ""What does IDK stand for?""",
@"Brunette: ""I dont know.""",
@"Blonde: ""OMG, nobody does!""",
@"Q: What's the difference between a Jew and a boy scout?A: A boy scout comes home from camp.",
@"Q. What did the elephant say to the naked man? A. ""How do you breathe through something so small?""",
@"An organization is like a tree full of monkeys, all on different limbs at different levels. The monkeys on top look down and see a tree full of smiling faces. The monkeys on the bottom look up and see nothing but assholes.",
@"Yo momma is so fat that when she went to the beach a whale swam up and sang, ""We are family, even though you're fatter than me.""",
@"The teller shrugged his shoulders and said, ""Fluctuations."" The Asian lady says, ""Fluc you white people too!""",
@"Q: What do a Christmas tree and a priest have in common? A: Their balls are just for decoration.",
@"Yo mamma is so ugly when she tried to join an ugly contest they said, ""Sorry, no professionals.""",
@"Q: What's the difference between a black man and a park bench? A: A park bench can support a family of four.",
@"Teacher: ""Kids,what does the chicken give you?""Student: ""Meat!""Teacher: ""Very good! Now what does the pig give you?""Student: ""Bacon!""Teacher: ""Great! And what does the fat cow give you?""Student: ""Homework!""",
@"Scientists have proven that there are two things in the air that have been known to cause women to get pregnant: their legs.",
@"Yo momma's so ugly, the government moved Halloween to her birthday!",
@"Q: How do Chinese people name their babies? A: They throw them down the stairs to see what noise they make.",
@"The teller shrugged his shoulders and said, ""Fluctuations."" The Asian lady says, ""Fluc you white people too!""",
@"Q: Why was six scared of seven? A: Because seven ""ate"" nine.",
@"Yo momma is so fat she uses a pillow for a tampon.",
@"Most people want a perfect relationship; I just want a hamburger that looks like ones in commercials."
            };
        }
    }
}
