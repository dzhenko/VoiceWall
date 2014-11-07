using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoiceWall.Web.ViewModels
{
    public static class FakeDataSeeder
    {
        private static int last = 0;
        private static Random random = new Random();
        private static ContentType lastType = ContentType.Picture;

        public static IEnumerable<WallItemViewModel> GetWallItems(int number)
        {
            var wallItems = new List<WallItemViewModel>();

            for (int i = 0; i < number; i++)
            {
                wallItems.Add(new WallItemViewModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = Guid.NewGuid().ToString(),
                    ContentUrl = GetContentUrl(),
                    ContentType = lastType,
                    Created = GetDate(),
                    Flags = random.Next(0, 3),
                    Likes = random.Next(15, 33),
                    Hates = random.Next(2, 13),
                    Views = random.Next(45, 124),
                    IsLiked = GetIsLiked(),
                    IsFlagged = GetIsFlagged(),
                    UserName = GetUsername(GetNext()),
                    UserImage = GetProfilePicture(GetSame()),
                    Comments = GetComments(random.Next(5, 15))
                });
            }

            return wallItems;
        }

        private static bool GetIsFlagged()
        {
            return random.Next(0, 100) < 20;
        }

        private static bool? GetIsLiked()
        {
            var num = random.Next(0, 100);
            if (num > 60)
            {
                return true;
            }
            else if (num > 15)
            {
                return null;
            }

            return false;
        }

        private static IEnumerable<Comment> GetComments(int number)
        {
            var comments = new List<Comment>();
            for (int i = 0; i < number; i++)
            {
                comments.Add(new Comment()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = Guid.NewGuid().ToString(),
                    Flags = random.Next(0, 2),
                    IsFlagged = random.Next(0, 100) < 15,
                    UserName = GetUsername(GetNext()),
                    UserImage = GetProfilePicture(GetSame()),
                    ContentUrl = GetContentUrl(),
                    ContentType = lastType,
                    Created = GetDate()
                });
            }

            return comments;
        }

        private static DateTime GetDate()
        {
            var date = DateTime.Now;
            date.AddDays((-1) * random.Next(0, 10));
            date.AddHours((-1) * random.Next(0, 23));
            date.AddMinutes((-1) * random.Next(0, 60));
            return date;
        }

        private static string GetContentUrl()
        {
            var rnd = GetNext() % 10;
            if (rnd > 5)
            {
                lastType = ContentType.Picture;
                return GetImageUrl(GetNext());
            }
            else if (rnd > 1)
            {
                lastType = ContentType.Sound;
                return GetSoundUrl(GetNext());
            }
            else
            {
                lastType = ContentType.Video;
                return GetVideoUrl(GetNext());
            }
        }

        private static int GetNext()
        {
            last = random.Next(0, 999);
            return last;
        }

        private static int GetSame()
        {
            return last;
        }

        private static string GetProfilePicture(int random)
        {
            var allImages = new List<string>()
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

            return allImages[(random % allImages.Count)];
        }

        private static string GetUsername(int random)
        {
            var allNames = new List<string>()
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

            return allNames[(random % allNames.Count)];
        }

        private static string GetSoundUrl(int random)
        {
            var allSounds = new List<string>()
            {
                "http://www.music.helsinki.fi/tmt/opetus/uusmedia/esim/a2002011001-e02.wav",
                "http://www.music.helsinki.fi/tmt/opetus/uusmedia/esim/a2002011001-e02-16kHz.wav",
                "http://www.music.helsinki.fi/tmt/opetus/uusmedia/esim/a2002011001-e02-8kHz.wav",
                "http://www.music.helsinki.fi/tmt/opetus/uusmedia/esim/a2002011001-e02-ulaw.wav",
                "http://www.music.helsinki.fi/tmt/opetus/uusmedia/esim/a2002011001-e02-128k.mp3"
            };

            return allSounds[(random % allSounds.Count)];
        }

        private static string GetVideoUrl(int random)
        {
            var allVideos = new List<string>()
            {
                "http://techslides.com/demos/sample-videos/small.webm",
                "http://video.webmfiles.org/big-buck-bunny_trailer.webm",
                "http://video.webmfiles.org/elephants-dream.webm",
                "http://easyhtml5video.com/images/happyfit2.webm"
            };

            return allVideos[(random % allVideos.Count)];
        }

        private static string GetImageUrl(int random)
        {
            var allImages = new List<string>()
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

            return allImages[(random % allImages.Count)];
        }
    }
}