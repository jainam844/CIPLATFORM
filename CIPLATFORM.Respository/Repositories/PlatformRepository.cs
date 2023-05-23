using CIPLATFORM.Entities.Data;
using CIPLATFORM.Entities.Models;
using CIPLATFORM.Respository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIPLATFORM.Entities.ViewModels;
using MimeKit;
using MimeKit.Text;
using MailKit.Security;
using System.Net.Mail;

using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using Microsoft.AspNetCore.Http;

namespace CIPLATFORM.Respository.Repositories
{
    public class PlatformRepository : IPlatformRepository
    {

        public readonly CiPlatformContext _CiPlatformContext;

        public PlatformRepository(CiPlatformContext CiPlatformContext)
        {
            _CiPlatformContext = CiPlatformContext;
        }


        public List<Country> GetCountryData()
        {
            List<Country> country = _CiPlatformContext.Countries.ToList();
            return country;
        }
        public List<City> GetCityData(List<int>? countryId)
        {

            List<City> city = _CiPlatformContext.Cities.Where(i => countryId.Contains((int)i.CountryId)).ToList();
            if (countryId.Count == 0)
                city = _CiPlatformContext.Cities.ToList();
            return city;
        }
        public List<City> GetCitys()
        {
            List<City> cities = _CiPlatformContext.Cities.ToList();
            return cities;
        }

        public List<MissionTheme> GetMissionThemes()
        {
            List<MissionTheme> themes = _CiPlatformContext.MissionThemes.ToList();
            return themes;

        }
        public List<MissionSkill> GetSkills()
        {

            var skills = _CiPlatformContext.MissionSkills.Include(m => m.Skill).ToList();
            return skills;

        }
        public List<Mission> GetMissionDetails()
        {
            List<Mission> missionDetails = _CiPlatformContext.Missions.Include(m => m.City).Include(m => m.Theme).Include(m => m.MissionMedia).Include(m => m.MissionRatings).Include(m => m.GoalMissions).Include(x => x.MissionApplications).Include(m => m.MissionSkills).Include(m => m.FavoriteMissions).Include(m => m.MissionApplications).Where(x => x.DeletedAt == null).ToList();
            return missionDetails;
        }


        public CardsViewModel getCards()
        {
            List<Mission> missions = _CiPlatformContext.Missions.Where(x => x.DeletedAt == null).Include(x => x.MissionApplications).ToList();
            List<MissionMedium> media = _CiPlatformContext.MissionMedia.Where(x => x.Default == 1).ToList();
            List<MissionSkill> missionSkills = _CiPlatformContext.MissionSkills.ToList();
            List<MissionTheme> missionThemes = _CiPlatformContext.MissionThemes.ToList();
            List<MissionRating> rating = _CiPlatformContext.MissionRatings.ToList();
            List<City> cities = _CiPlatformContext.Cities.ToList();
            List<Country> countries = _CiPlatformContext.Countries.ToList();
            List<FavoriteMission> favoriteMission = _CiPlatformContext.FavoriteMissions.ToList();
            List<User> users = _CiPlatformContext.Users.Where(x => x.DeletedAt == null).ToList();
            CardsViewModel missionCards = new CardsViewModel();

            {

                missionCards.missions = missions;
                missionCards.missionthemes = missionThemes;
                missionCards.missionskill = missionSkills;
                missionCards.media = media;
                missionCards.rating = rating;
                missionCards.countries = countries;
                missionCards.cities = cities;
                missionCards.favoriteMissions = favoriteMission;
                missionCards.coworkers = users;
            }
            return missionCards;

        }
        public MissionListingViewModel GetCardDetail(int mid, int uid)
        {
            List<Mission> missions = GetMissionDetails();
            Mission mission = missions.FirstOrDefault(x => x.MissionId == mid);

            List<MissionMedium> photos = media(mid);
            List<MissionDocument> documents = document(mid);

            List<MissionSkill> missionSkills = _CiPlatformContext.MissionSkills.Include(m => m.Skill).Where(x => x.MissionId == mid).ToList();
            List<MissionApplication> applications = _CiPlatformContext.MissionApplications.Include(m => m.User).Where(x => x.MissionId == mid).ToList();

            List<Mission> relatedMissions = missions.Where(a => a.MissionId != mission.MissionId &&
                                           (a.ThemeId == mission.ThemeId || (a.CityId == mission.CityId || a.CountryId == mission.CountryId ||
                                           a.OrganizationName == mission.OrganizationName)))
                                           .OrderByDescending(a => a.ThemeId == mission.ThemeId)
                                           .ThenByDescending(a => a.CityId == mission.CityId)
                                           .ThenByDescending(a => a.CountryId == mission.CountryId)
                                           .ThenByDescending(a => a.OrganizationName == mission.OrganizationName)
                                           .Take(3)
                                           .ToList();


            List<Comment> comments = _CiPlatformContext.Comments.Include(m => m.User).Where(x => x.MissionId == mid && x.ApprovalStatus == "Published").ToList();
            List<User> users = _CiPlatformContext.Users.ToList();


            List<User> allUser = _CiPlatformContext.Users.Where(x => x.DeletedAt == null).ToList();
            List<MissionInvite> alreaduInvite = _CiPlatformContext.MissionInvites.Where(x => x.MissionId == mid && x.FromUserId == uid).Include(x => x.ToUser).ToList();
            foreach (var i in alreaduInvite)
            {
                allUser = allUser.Where(x => x.UserId != i.ToUserId).ToList();
            }

            List<FavoriteMission> favoriteMission = _CiPlatformContext.FavoriteMissions.ToList();
            MissionListingViewModel CardDetail = new MissionListingViewModel();
            {
                CardDetail.missions = mission;
                CardDetail.missionmedias = photos;
                CardDetail.missiondocuments = documents;
                CardDetail.missionapplications = applications;
                CardDetail.missionskills = missionSkills;
                CardDetail.relatedmissions = relatedMissions;
                CardDetail.comments = comments;
                CardDetail.coworkers = allUser;
                CardDetail.alreadyinvite = alreaduInvite;
                CardDetail.favoriteMissions = favoriteMission;
            }

            return CardDetail;
        }

        public bool addToFav(int missionId, int userId)
        {
            FavoriteMission favorite = new();
            favorite.MissionId = missionId;
            favorite.UserId = userId;

            var fM = _CiPlatformContext.FavoriteMissions.FirstOrDefault(f => f.MissionId == missionId && f.UserId == userId);

            if (fM == null)
            {

                _CiPlatformContext.FavoriteMissions.Add(favorite);
                return true;
            }

            else
            {
                _CiPlatformContext.FavoriteMissions.Remove(fM);
                return false;

            }
        }
        public List<MissionDocument> document(int mid)
        {
            List<MissionDocument> documents = _CiPlatformContext.MissionDocuments.Where(x => x.MissionId == mid).ToList();
            return documents;
        }
        public List<MissionMedium> media(int mid)
        {
            List<MissionMedium> photos = _CiPlatformContext.MissionMedia.Where(x => x.MissionId == mid).ToList();
            return photos;
        }
        public int GetMissionCount()
        {

            int missionNumber = _CiPlatformContext.Missions.Where(x => x.DeletedAt == null).Count();
            return missionNumber;

        }
        public int GetnotificationCount(int uid)
        {

            int missionNumber = getnotification(uid).Count;
            return missionNumber;

        }


        //Star Rating
        public bool MissionRating(int userId, int mid, int rating)
        {
            MissionApplication ma = _CiPlatformContext.MissionApplications.FirstOrDefault(a => a.UserId == userId && a.MissionId == mid && a.ApprovalStatus == "Approve");
            if (ma != null)
            {


                MissionRating CheckRating = _CiPlatformContext.MissionRatings.FirstOrDefault(a => a.UserId == userId && a.MissionId == mid);
                if (CheckRating != null)
                {

                    //CheckRating.UserId = userId;
                    //CheckRating.MissionId = mid;
                    CheckRating.Rating = rating;
                    CheckRating.UpdatedAt = DateTime.Now;

                    _CiPlatformContext.Update(CheckRating);
                    _CiPlatformContext.SaveChanges();

                    return false;
                }
                else
                {
                    MissionRating missionRating = new MissionRating();
                    missionRating.UserId = userId;
                    missionRating.MissionId = mid;
                    missionRating.Rating = rating;


                    _CiPlatformContext.Add(missionRating);
                    _CiPlatformContext.SaveChanges();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public void addComment(int mid, int uid, string comnt)
        {
            Comment comment = new Comment();

            comment.MissionId = mid;
            comment.UserId = uid;
            comment.CommentDescription = comnt;

            _CiPlatformContext.Comments.Add(comment);
            _CiPlatformContext.SaveChanges();

        }


        public bool applyMission(int mid, int uid)
        {
            MissionApplication application = new();
            application.MissionId = mid;
            application.UserId = uid;

            var applicable = _CiPlatformContext.MissionApplications.FirstOrDefault(a => a.MissionId == mid && a.UserId == uid);

            if (applicable == null)
            {
                application.CreatedAt = DateTime.Now;
                application.AppliedAt = DateTime.Now;
                _CiPlatformContext.MissionApplications.Add(application);
                _CiPlatformContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool MIcheck(int mid, int userId, List<int> ToUserId)
        {
            MissionInvite mi = new MissionInvite();
            {
                foreach (var item in ToUserId)
                {
                    mi = _CiPlatformContext.MissionInvites.FirstOrDefault(x => x.MissionId == mid && x.FromUserId == userId && x.ToUserId == item);

                }
                if (mi == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void RecommandToCoWorker(int FromUserId, List<int> ToUserId, int mid)
        {
            var fromUser = _CiPlatformContext.Users.FirstOrDefault(u => u.UserId == FromUserId && u.DeletedAt == null);
            var fromEmailId = fromUser.Email;


            foreach (var user in ToUserId)
            {

                var toUser = _CiPlatformContext.Users.FirstOrDefault(u => u.UserId == user && u.DeletedAt == null);

                //NotificationSetting check = _CiPlatformContext.NotificationSettings.FirstOrDefault(x => x.UserId == user);

                var toEmailId = toUser.Email;

                MissionInvite invite = new MissionInvite();
                {
                    invite.FromUserId = FromUserId;
                    invite.ToUserId = user;
                    invite.MissionId = mid;
                }
                _CiPlatformContext.Add(invite);
                _CiPlatformContext.SaveChanges();
                bool msg = _CiPlatformContext.NotificationSettings.Any(x => x.UserId == user);
                if (msg)
                {
                    NotificationSetting check = _CiPlatformContext.NotificationSettings.FirstOrDefault(x => x.UserId == user);
                    if (check.RecommendedMission == true)
                    {
                        NotificationMessage nm = new NotificationMessage();
                        {
                            nm.UserId = user;
                            nm.Message = fromUser.FirstName + " Has Recommanded You To This Mission: Check This out";
                            nm.Type = "RecommendedMission";
                            nm.Id = mid;
                        }
                        _CiPlatformContext.NotificationMessages.Add(nm);
                        _CiPlatformContext.SaveChanges();
                    }
                }
                #region Send Mail
                var mailBody = "<h1></h1><br><h2><a href='" + "https://localhost:7028/Platform/MissionListing?mid=" + mid + "'>Check Out this Mission!</a></h2>";

                // create email message
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(fromEmailId));
                email.To.Add(MailboxAddress.Parse(toEmailId));
                email.Subject = "Recommand to Mission";
                email.Body = new TextPart(TextFormat.Html) { Text = mailBody };

                // send email
                using var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("jainamshah492@gmail.com", "aflpkkevlfxzmmtx");
                smtp.Send(email);
                smtp.Disconnect(true);
                #endregion Send Mail
            }
        }

        public StoryListingViewModel GetStoryDetail()
        {
            List<Story> stories = _CiPlatformContext.Stories.Include(m => m.User).Include(m => m.StoryMedia).Include(m => m.Mission).Where(m => m.Status == "PUBLISHED").ToList();


            //List<StoryMedium> photos = smedia(sid);
            StoryListingViewModel StoryDetail = new StoryListingViewModel();
            {
                StoryDetail.stories = stories;
                //StoryDetail.storymedias = photos;
            }


            return StoryDetail;
        }

        public StoryView addView(int sid, int UId)
        {
            StoryView sv = _CiPlatformContext.StoryViews.FirstOrDefault(x => x.StoryId == sid && x.UserId == UId);
            if (sv == null)
            {
                StoryView sv1 = new StoryView();
                {
                    sv1.StoryId = sid;
                    sv1.UserId = UId;
                    _CiPlatformContext.StoryViews.Add(sv1);
                    _CiPlatformContext.SaveChanges();
                }
                return sv1;
            }
            else
            {
                return sv;
            }

        }


        public StoryListingViewModel GetStory(int sid, int uid)
        {

            Story story = _CiPlatformContext.Stories.Include(m => m.User).Include(m => m.StoryViews).FirstOrDefault(m => m.StoryId == sid);
            List<StoryMedium> photos = smedia(sid);
            List<User> users = _CiPlatformContext.Users.ToList();


            List<User> allUser = _CiPlatformContext.Users.Where(x => x.DeletedAt == null).ToList();
            List<StoryInvite> alreaduInvite = _CiPlatformContext.StoryInvites.Where(x => x.StoryId == sid && x.FromUserId == uid).Include(x => x.ToUser).ToList();
            foreach (var i in alreaduInvite)
            {
                allUser = allUser.Where(x => x.UserId != i.ToUserId).ToList();
            }
            StoryListingViewModel StoryDetail = new StoryListingViewModel();
            {
                StoryDetail.storymedias = photos;
                StoryDetail.story = story;
                StoryDetail.coworkers = allUser;
                StoryDetail.alreadyinvite = alreaduInvite;
            }
            return StoryDetail;
        }
        public List<StoryMedium> smedia(int sid)
        {
            List<StoryMedium> photos = _CiPlatformContext.StoryMedia.Where(x => x.StoryId == sid && x.Type == "png").ToList();
            return photos;
        }
        public bool MIcheckStory(int sid, int userId, List<int> ToUserId)
        {
            StoryInvite si = new StoryInvite();
            {
                foreach (var item in ToUserId)
                {
                    si = _CiPlatformContext.StoryInvites.FirstOrDefault(x => x.StoryId == sid && x.FromUserId == userId && x.ToUserId == item);

                }
                if (si == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public void RecommandStory(int FromUserId, List<int> ToUserId, int sid)
        {
            var fromUser = _CiPlatformContext.Users.FirstOrDefault(u => u.UserId == FromUserId && u.DeletedAt == null);

            var fromEmailId = fromUser.Email;
           
            //if (user1 == null)
            //{
            //    return null;
            //}

            foreach (var user in ToUserId)
            {
                var toUser = _CiPlatformContext.Users.FirstOrDefault(u => u.UserId == user && u.DeletedAt == null);
                NotificationSetting check = _CiPlatformContext.NotificationSettings.FirstOrDefault(x => x.UserId == user);
                if (check == null)
                {
                    NotificationSetting ns = new NotificationSetting();
                    ns.UserId = user;
                    ns.NewMission = true;
                    ns.EmailNotification = true;
                    ns.MissionApplication = true;
                    ns.RecommendedMission = true;
                    ns.RecommendedStory = true;
                    ns.Story = true;
                    _CiPlatformContext.NotificationSettings.Add(ns);
                    _CiPlatformContext.SaveChanges();

                }
                var toEmailId = toUser.Email;
                var messages = _CiPlatformContext.NotificationSettings
.FromSql($"exec [GetNotificationSetting1] @userId={user}")
.AsEnumerable()
.FirstOrDefault();
                StoryInvite invite = new StoryInvite();
                {
                    invite.FromUserId = FromUserId;
                    invite.ToUserId = user;
                    invite.StoryId = sid;

                }
                _CiPlatformContext.Add(invite);
                _CiPlatformContext.SaveChanges();

                if (messages.RecommendedMission == true)
                {
                    NotificationMessage nm = new NotificationMessage();
                    {
                        nm.UserId = user;
                        nm.Message = fromUser.FirstName + " Has Recommanded You To This Story Check This out";
                        nm.Type = "RecommendedStory";
                        nm.Id = sid;
                    }
                    _CiPlatformContext.NotificationMessages.Add(nm);
                    _CiPlatformContext.SaveChanges();
                }


                #region Send Mail
                var mailBody = "<h1></h1><br><h2><a href='" + "https://localhost:7028/Platform/StoryDetail?sid=" + sid + "'>Check Out this Mission!</a></h2>";

                // create email message
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(fromEmailId));
                email.To.Add(MailboxAddress.Parse(toEmailId));
                email.Subject = "Recommand To Story";
                email.Body = new TextPart(TextFormat.Html) { Text = mailBody };

                // send email
                using var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("jainamshah492@gmail.com", "aflpkkevlfxzmmtx");
                smtp.Send(email);
                smtp.Disconnect(true);
                #endregion Send Mail
            }
        }

        public List<MissionApplication> Mission(int UId)
        {
            List<MissionApplication> missions = _CiPlatformContext.MissionApplications.Include(m => m.Mission).Where(x => x.UserId == UId && x.ApprovalStatus == "Approve").ToList();
            return missions;
        }

        public StoryListingViewModel ShareStory(int UId)
        {
            List<MissionApplication> mission = Mission(UId);
            StoryListingViewModel StoryDetail = new StoryListingViewModel();
            {
                StoryDetail.missions = mission;
            }
            return StoryDetail;

        }


        public bool saveStory(StoryListingViewModel obj, int status, int uid)
        {
            Story story = _CiPlatformContext.Stories.FirstOrDefault(x => x.UserId == uid && x.MissionId == obj.story.MissionId);

            if (story == null)
            {
                Story str = new Story();
                {
                    str.Title = obj.story.Title;
                    str.PublishedAt = obj.story.PublishedAt;
                    str.Description = obj.story.Description;
                    str.UserId = uid;
                    str.MissionId = obj.story.MissionId;

                }

                if (status == 1)
                {
                    str.Status = "DRAFT";
                }
                if (status == 2)
                {
                    str.Status = "PENDING";
                }

                _CiPlatformContext.Stories.Add(str);
                _CiPlatformContext.SaveChanges();
            }

            if (story != null)
            {

                {
                    story.Title = obj.story.Title;
                    story.PublishedAt = obj.story.PublishedAt;
                    story.Description = obj.story.Description;
                    story.UserId = uid;
                    story.MissionId = obj.story.MissionId;
                    story.UpdatedAt = DateTime.Now;
                }

                if (status == 1)
                {
                    story.Status = "DRAFT";
                }
                if (status == 2)
                {
                    story.Status = "PENDING";
                }

                _CiPlatformContext.Stories.Update(story);
                _CiPlatformContext.SaveChanges();
            }

            return true;
        }
        public bool SaveImage(StoryListingViewModel obj, List<IFormFile> file)
        {
            Story xyz = _CiPlatformContext.Stories.FirstOrDefault(x => x.Title == obj.story.Title);
            if (file != null)
            {
                List<StoryMedium> check = _CiPlatformContext.StoryMedia.Where(x => x.StoryId == xyz.StoryId && x.Type == "png").ToList();
                _CiPlatformContext.StoryMedia.RemoveRange(check);

                List<string> filePaths = new List<string>();

                foreach (var formFile in file)
                {
                    StoryMedium mediaobj = new StoryMedium();
                    mediaobj.StoryId = xyz.StoryId;
                    mediaobj.Path = formFile.FileName;
                    mediaobj.Type = "png";

                    _CiPlatformContext.StoryMedia.Add(mediaobj);
                    _CiPlatformContext.SaveChanges();

                    if (formFile.Length > 0)
                    {

                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/A", formFile.FileName); //we are using Temp file name just for the example. Add your own file path.
                        if (File.Exists(filePath) == false)
                        {
                            filePaths.Add(filePath);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                formFile.CopyToAsync(stream);
                            }
                        }

                    }
                }
            }
            if (obj.url != null)
            {
                var checkurl = _CiPlatformContext.StoryMedia.Where(m => m.StoryId == xyz.StoryId && m.Type == "video").FirstOrDefault();

                if (checkurl != null)
                {
                    checkurl.Path = obj.url;
                    checkurl.UpdatedAt = DateTime.Now;

                    _CiPlatformContext.StoryMedia.Update(checkurl);
                    _CiPlatformContext.SaveChanges();
                }
                else
                {
                    StoryMedium forUrl = new StoryMedium();
                    {
                        forUrl.StoryId = xyz.StoryId;
                        forUrl.Type = "video";
                        forUrl.Path = obj.url;
                    }
                    _CiPlatformContext.StoryMedia.Add(forUrl);
                    _CiPlatformContext.SaveChanges();
                }
            }
            return true;
        }

        public StoryListingViewModel getData(int mid, int uid)
        {
            StoryListingViewModel obj = new StoryListingViewModel();
            Story story = _CiPlatformContext.Stories.FirstOrDefault(m => m.MissionId == mid && m.UserId == uid && m.Status == "DRAFT");


            if (story != null)
            {
                List<StoryMedium> simgs = _CiPlatformContext.StoryMedia.Where(m => m.StoryId == story.StoryId && m.Type == "png").ToList();
                StoryMedium? url = _CiPlatformContext.StoryMedia.FirstOrDefault(m => m.StoryId == story.StoryId && m.Type == "video");
                {
                    obj.story = story;
                    if (url != null)
                    {
                        obj.url = url.Path;
                    }
                }
                foreach (var item in simgs)
                {
                    obj.simg.Add(item.Path);

                    story.StoryMedia.Remove(item);
                }
                story.StoryMedia.Remove(url);
                return obj;
            }

            return null;
        }

        public List<Story> StoryFilter(string? search, int pg)
        {
            var pageSize = 3;
            List<Story> cards = new List<Story>();
            var missioncards = _CiPlatformContext.Stories.Include(m => m.StoryMedia).Include(m => m.Mission).Include(m => m.Mission.Theme).Include(m => m.User).ToList();
            var Missionskills = _CiPlatformContext.MissionSkills.Include(m => m.Skill).ToList();
            List<int> temp = new List<int>();



            if (search != null)
            {
                search = search.ToLower();
                missioncards = missioncards.Where(x => x.Title.ToLower().Contains(search)).ToList();


            }
            if (pg != 0)
            {
                missioncards = missioncards.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
            }
            return missioncards;


        }

        public List<Mission> Filter(List<int>? cityId, List<int>? countryId, List<int>? themeId, List<int>? skillId, string? search, int? sort, int pg, int UId)
        {
            var pageSize = 3;

            List<Mission> cards = new List<Mission>();
            var missioncards = GetMissionDetails();
            var Missionskills = GetSkills();
            List<Mission> temp = new List<Mission>();

            if (search != null)
            {
                search = search.ToLower();
                missioncards = missioncards.Where(x => x.Title.ToLower().Contains(search)).ToList();

            }
            if (countryId.Count > 0)
            {

                missioncards = missioncards.Where(c => countryId.Contains((int)c.CountryId)).ToList();

            }
            if (cityId.Count > 0)
            {
                missioncards = missioncards.Where(c => cityId.Contains((int)c.CityId)).ToList();
            }
            if (themeId.Count > 0)

            {
                missioncards = missioncards.Where(c => themeId.Contains((int)c.ThemeId)).ToList();

            }
            if (skillId.Count > 0)
            {
                foreach (var n in skillId)
                {
                    temp.AddRange(missioncards.Where(x => x.MissionSkills.Any(x => x.SkillId == n)));
                }
                missioncards = temp;
            }
            if (sort != null)
            {
                if (sort == 1)
                {

                    missioncards = missioncards.OrderByDescending(x => x.CreatedAt).ToList();

                }
                if (sort == 2)
                {

                    missioncards = missioncards.OrderBy(x => x.CreatedAt).ToList();

                }
                if (sort == 3)
                {
                    missioncards = missioncards.Where(x => x.FavoriteMissions.Any(x => x.UserId == UId)).ToList();

                }
                if (sort == 4)
                {
                    missioncards = missioncards.Where(x => x.MissionType == "Goal").OrderBy(i => i.GoalMissions.FirstOrDefault().GoalValue).ToList();
                }
                if (sort == 5)
                {
                    missioncards = missioncards.Where(x => x.MissionType == "Goal").OrderByDescending(i => i.GoalMissions.FirstOrDefault().GoalValue).ToList();
                }
                if (sort == 6)
                {
                    List<MissionTheme> missionThemes = _CiPlatformContext.MissionThemes.Include(m => m.Missions).OrderByDescending(m => m.Missions.Count).ToList();
                    foreach (var theme in missionThemes)
                    {
                        temp.AddRange(missioncards.Where(m => m.Theme == theme));
                    }
                    missioncards = temp;
                }
                if (sort == 7)
                {
                    missioncards = _CiPlatformContext.Missions.Include(m => m.MissionRatings).Where(m => m.DeletedAt == null).OrderByDescending(m => m.MissionRatings.Average(r => r.Rating)).ToList();
                }
                if (sort == 8)
                {
                    missioncards = missioncards.OrderByDescending(m => m.FavoriteMissions.Count).ToList();
                }
                if (sort == 9)
                {
                    missioncards = _CiPlatformContext.Missions.Where(m => m.DeletedAt == null).ToList();
                    Random random = new Random();

                    // Shuffle the missioncards list using the Fisher-Yates algorithm
                    for (int i = missioncards.Count - 1; i >= 1; i--)
                    {
                        // Generate a random index between 0 and i (inclusive)
                        int j = random.Next(i + 1);

                        // Swap the elements at indices i and j
                        Mission temp1 = missioncards[i];
                        missioncards[i] = missioncards[j];
                        missioncards[j] = temp1;
                    }
                }



            }
            if (pg != 0)
            {
                missioncards = missioncards.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
            }

            return missioncards;

        }
        public List<MissionListingViewModel> getMisAppList(int pg, long missionId)
        {
            var pageSize = 1;
            List<MissionApplication> missionApplications = _CiPlatformContext.MissionApplications.Where(m => m.ApprovalStatus == "Approve" && m.MissionId == missionId).Include(x => x.User).ToList();
            List<MissionListingViewModel> misView = new List<MissionListingViewModel>();
            foreach (MissionApplication app in missionApplications)
            {
                MissionListingViewModel mView = new MissionListingViewModel();
                User user = _CiPlatformContext.Users.FirstOrDefault(u => u.UserId == app.UserId);
                mView.MissionId = missionId;
                mView.Avatar = user.Avatar;
                mView.UserName = user.FirstName + " " + user.LastName;
                misView.Add(mView);
            }

            if (pg != 0)
            {
                misView = misView.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
            }
            return misView;
        }

        public void settings(string[] settings, int uid)
        {
            NotificationSetting check = _CiPlatformContext.NotificationSettings.FirstOrDefault(x => x.UserId == uid);
            if (check == null)
            {
                check = new NotificationSetting();
                check.UserId = uid;

            }
            if (settings.Contains("RecommendedMission"))
            {
                check.RecommendedMission = true;
            }
            else
            {
                check.RecommendedMission = false;
            }
            if (settings.Contains("Story"))
            {
                check.Story = true;
            }
            else
            {
                check.Story = false;
            }
            if (settings.Contains("NewMission"))
            {
                check.NewMission = true;
            }
            else
            {
                check.NewMission = false;
            }
            if (settings.Contains("RecommendedStory"))
            {
                check.RecommendedStory = true;
            }
            else
            {
                check.RecommendedStory = false;
            }
            if (settings.Contains("MissionApplication"))
            {
                check.MissionApplication = true;
            }
            else
            {
                check.MissionApplication = false;
            }
            if (settings.Contains("EmailNotification"))
            {
                check.EmailNotification = true;
            }
            else
            {
                check.EmailNotification = false;
            }
            if (check != null)
            {
                check.UpdatedAt = DateTime.Now;
            }
            _CiPlatformContext.Update(check);
            _CiPlatformContext.SaveChanges();

        }
        public NotificationSetting getsettings(int uid)
        {
            NotificationSetting ns = _CiPlatformContext.NotificationSettings.FirstOrDefault(x => x.UserId == uid);
            return ns;
        }
        public List<NotificationMessage> getnotification(int uid)
        {
            NotificationSetting ns = _CiPlatformContext.NotificationSettings.FirstOrDefault(m => m.UserId == uid);


            //List<NotificationMessage> messages = _CiPlatformContext.NotificationMessages.Include(m => m.User).Include(m => m.User.MissionInviteToUsers).Include(m => m.User.MissionInviteFromUsers).Where(x => x.UserId == uid && x.Status != "Cleared").ToList();
            var messages = _CiPlatformContext.NotificationMessages.FromSql($"exec [GetNotificationMessages1]  @userId={uid}").ToList();

            if (ns.MissionApplication != true)
            {
                messages = messages.Where(m => m.Type != "MissionApplication").ToList();
            }
            if (ns.Story != true)
            {
                messages = messages.Where(m => m.Type != "Story").ToList();


            }
            if (ns.RecommendedMission != true)
            {
                messages = messages.Where(m => m.Type != "RecommendedMission").ToList();

            }
            if (ns.RecommendedStory != true)
            {
                messages = messages.Where(m => m.Type != "RecommendedStory").ToList();

            }
            if (ns.NewMission != true)
            {
                messages = messages.Where(m => m.Type != "NewMission").ToList();

            }

         


            return messages;
        }

        public void readNotification(int id)
        {
            NotificationMessage nm = _CiPlatformContext.NotificationMessages.FirstOrDefault(m => m.NotificationMessageId == id);
            nm.Status = "Read";
            nm.UpdatedAt = DateTime.Now;
            _CiPlatformContext.NotificationMessages.Update(nm);
            _CiPlatformContext.SaveChanges();
        }
        public void clearNotification(int uid)
        {
            List<NotificationMessage> nm = _CiPlatformContext.NotificationMessages.Where(m => m.UserId == uid).ToList();
            foreach (NotificationMessage m in nm)
            {
                m.Status = "Cleared";
                m.UpdatedAt = DateTime.Now;
                _CiPlatformContext.NotificationMessages.Update(m);
                _CiPlatformContext.SaveChanges();
            }
        }


        public bool SendMail(NotificationMessage message)
        {
            string toUser = _CiPlatformContext.Users.FirstOrDefault(m => m.UserId == message.UserId).Email;
            #region Send Mail
            string link = null;
            if (message.Type == "MissionApplication" || message.Type == "NewMission")
            {
                link = "https://localhost:7028/Platform/MissionListing?mid=";
            }
            if (message.Type == "Story")
            {
                link = "https://localhost:7028/Platform/StoryDetail?sid=";
            }

            var mailBody = "<h1>" + message.Message + "</h1><br><h2><a href='" + link + message.Id + "'>Check Out this Mission!</a></h2>";

            // create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("jainamshah492@gmail.com"));
            email.To.Add(MailboxAddress.Parse(toUser));
            email.Subject = message.Type;
            email.Body = new TextPart(TextFormat.Html) { Text = mailBody };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("jainamshah492@gmail.com", "aflpkkevlfxzmmtx");
            smtp.Send(email);
            smtp.Disconnect(true);
            #endregion Send Mail
            return true;
        }
    }
}
