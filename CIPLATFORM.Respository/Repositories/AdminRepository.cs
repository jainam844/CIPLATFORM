using CIPLATFORM.Entities.Data;
using CIPLATFORM.Entities.Models;
using CIPLATFORM.Entities.ViewModels;
using CIPLATFORM.Respository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace CIPLATFORM.Respository.Repositories
{
    public class AdminRepository : IAdminRepository
    {

        public readonly CiPlatformContext _CiPlatformContext;

        public AdminRepository(CiPlatformContext CiPlatformContext)
        {
            _CiPlatformContext = CiPlatformContext;
        }
        public AdminViewModel getData()
        {
            AdminViewModel um = new AdminViewModel();
            um.users = _CiPlatformContext.Users.Where(x => x.DeletedAt == null).ToList();
            um.cities = _CiPlatformContext.Cities.ToList();
            um.countries = _CiPlatformContext.Countries.ToList();
            um.cmspages = _CiPlatformContext.CmsPages.Where(x => x.DeletedAt == null).ToList();
            um.missions = _CiPlatformContext.Missions.Where(x => x.DeletedAt == null).ToList();
            um.missionthemes = _CiPlatformContext.MissionThemes.Where(x => x.DeletedAt == null).ToList();
            um.newskills = _CiPlatformContext.Skills.Where(x => x.DeletedAt == null).ToList();
            um.newmissionThemes = _CiPlatformContext.MissionThemes.Where(x => x.DeletedAt == null).ToList();
            um.skills = _CiPlatformContext.Skills.Where(x => x.DeletedAt == null).ToList();
            um.missionapplications = _CiPlatformContext.MissionApplications.Include(x => x.Mission).Include(x => x.User).Where(x => x.ApprovalStatus == "Pending").ToList();
            um.stories = _CiPlatformContext.Stories.Include(x => x.User).Where(x => x.Status == "PENDING").Where(x => x.DeletedAt == null).ToList();
            um.banners = _CiPlatformContext.Banners.Where(x => x.DeletedAt == null).ToList();
            return um;
        }
        public AdminViewModel Usersearch(string search, int pg)
        {
            var pageSize = 5;
            AdminViewModel obj = getData();

            if (search != null)
            {
                search = search.ToLower();
                obj.users = obj.users.Where(x => x.FirstName.ToLower().Contains(search)).ToList();
                obj.cmspages = obj.cmspages.Where(x => x.Title.ToLower().Contains(search)).ToList();
                obj.missions = obj.missions.Where(x => x.Title.ToLower().Contains(search) || x.MissionType.ToLower().Contains(search)).ToList();
                obj.missionthemes = obj.missionthemes.Where(x => x.Title.ToLower().Contains(search)).ToList();
                //obj.missionSkills=obj.missionSkills.Where(x=>x.Skill.SkillName.ToLower().Contains(search)).ToList();
                obj.skills = obj.skills.Where(x => x.SkillName.ToLower().Contains(search)).ToList();
                obj.missionapplications = obj.missionapplications.Where(x => x.Mission.Title.ToLower().Contains(search)).ToList();
                obj.stories = obj.stories.Where(x => x.Title.ToLower().Contains(search)).ToList();
                //obj.banner = obj.banners.Where(x => x.SortOrder.Contains(search)).ToList();

            }
            if (pg != 0)
            {
                obj.users = obj.users.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
                obj.cmspages = obj.cmspages.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
                obj.missions = obj.missions.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
                obj.missionthemes = obj.missionthemes.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
                //obj.missionSkills = obj.missionSkills.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
                obj.skills = obj.skills.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
                obj.missionapplications = obj.missionapplications.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
                obj.stories = obj.stories.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
                obj.banners = obj.banners.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
            }
            return obj;


        }
        public bool addcms(AdminViewModel obj, int command)
        {
            if (command == 1)
            {
                if (obj.user.UserId == 0)
                {
                    User user = new User();
                    {
                        if (obj.Avatarfile != null)
                        {
                            user.Avatar = obj.Avatarfile.FileName;
                        }
                        user.FirstName = obj.user.FirstName;
                        user.LastName = obj.user.LastName;
                        user.Email = obj.user.Email;
                        user.Password = Crypto.HashPassword(obj.user.Password);
                        user.EmployeeId = obj.user.EmployeeId;
                        user.Department = obj.user.Department;
                        user.CountryId = obj.user.CountryId;
                        user.CityId = obj.user.CityId;
                        user.ProfileText = obj.user.ProfileText;
                        user.Status = obj.user.Status;
                    }
                    _CiPlatformContext.Add(user);
                    _CiPlatformContext.SaveChanges();
                    return true;
                }
                else
                {
                    User user = _CiPlatformContext.Users.FirstOrDefault(x => x.UserId == obj.user.UserId);
                    {
                        if (obj.Avatarfile != null)
                        {
                            user.Avatar = obj.Avatarfile.FileName;
                        }
                        user.FirstName = obj.user.FirstName;
                        user.LastName = obj.user.LastName;
                        user.Email = obj.user.Email;
                        user.EmployeeId = obj.user.EmployeeId;
                        user.Department = obj.user.Department;
                        user.CountryId = obj.user.CountryId;
                        user.CityId = obj.user.CityId;
                        user.ProfileText = obj.user.ProfileText;
                        user.Status = obj.user.Status;
                        user.UpdatedAt = DateTime.Now;
                    }
                    _CiPlatformContext.Update(user);
                    _CiPlatformContext.SaveChanges();
                    return false;
                }

            }
            if (command == 2)
            {
                if (obj.CmsPage.CmsPageId == 0)
                {
                    CmsPage cms = new CmsPage();
                    {
                        cms.Title = obj.CmsPage.Title;
                        cms.Description = obj.CmsPage.Description;
                        cms.Slug = obj.CmsPage.Slug;
                    }
                    _CiPlatformContext.Add(cms);
                    _CiPlatformContext.SaveChanges();
                    return true;
                }
                else
                {
                    CmsPage cms = _CiPlatformContext.CmsPages.FirstOrDefault(x => x.CmsPageId == obj.CmsPage.CmsPageId);
                    {
                        cms.Title = obj.CmsPage.Title;
                        cms.Description = obj.CmsPage.Description;
                        cms.Slug = obj.CmsPage.Slug;
                        cms.UpdatedAt = DateTime.Now;
                    }
                    _CiPlatformContext.Update(cms);
                    _CiPlatformContext.SaveChanges();
                    return false;
                }

            }
            if (command == 3)
            {
                if (obj.mission.MissionId == 0)
                {
                    Mission mission = new Mission();
                    List<NotificationSetting> check = _CiPlatformContext.NotificationSettings.Where(x => x.NewMission == true).ToList();
                    {
                        mission.Title = obj.mission.Title;
                        mission.Description = obj.mission.Description;
                        mission.ShortDescription = obj.mission.ShortDescription;
                        mission.CountryId = obj.mission.CountryId;
                        mission.CityId = obj.mission.CityId;
                        mission.OrganizationName = obj.mission.OrganizationName;
                        mission.OrganizationDetail = obj.mission.OrganizationDetail;
                        mission.StartDate = obj.mission.StartDate;
                        mission.EndDate = obj.mission.EndDate;
                        mission.MissionType = obj.mission.MissionType;
                        mission.Avaibility = obj.mission.Avaibility;
                        mission.ThemeId = obj.mission.ThemeId;

                        _CiPlatformContext.Missions.Add(mission);
                        _CiPlatformContext.SaveChanges();
                        foreach (var item in check)
                        {
                            NotificationMessage nm = new NotificationMessage();
                            {
                                nm.UserId = item.UserId;
                                nm.Message = "New Mission -" + mission.Title;
                                nm.Type = "NewMission";
                                nm.Id = mission.MissionId;
                            }
                            _CiPlatformContext.NotificationMessages.Add(nm);
                            _CiPlatformContext.SaveChanges();
                        }
                    }
                    if (obj.missionDocuments != null)
                    {
                        foreach (var item in obj.missionDocuments)
                        {
                            MissionMedium mm = new MissionMedium();
                            {
                                mm.MediaName = item.Name;
                                mm.MissionId = mission.MissionId;
                                mm.MediaPath = item.FileName;
                                mm.MediaType = "png";
                            }
                            _CiPlatformContext.MissionMedia.Add(mm);
                            _CiPlatformContext.SaveChanges();
                        }
                    }

                    if (obj.editmissionSkills != null)
                    {
                        foreach (var item in obj.editmissionSkills)
                        {
                            MissionSkill ms = new MissionSkill();
                            {
                                ms.SkillId = item;
                                ms.MissionId = mission.MissionId;
                            }
                            _CiPlatformContext.MissionSkills.Add(ms);
                            _CiPlatformContext.SaveChanges();
                        }
                    }

                    if (obj.url != null)
                    {


                        MissionMedium nmds = new MissionMedium();
                        {
                            nmds.MediaName = obj.url;
                            nmds.MissionId = mission.MissionId;
                            nmds.MediaPath = obj.url;
                            nmds.MediaType = "url";
                        }
                        _CiPlatformContext.MissionMedia.Add(nmds);
                        _CiPlatformContext.SaveChanges();
                    }
                    if (obj.missionDs != null)
                    {
                        foreach (var item in obj.missionDs)
                        {
                            MissionDocument md = new MissionDocument();
                            {
                                md.DocumentName = item.FileName;
                                md.MissionId = mission.MissionId;
                                md.DocumentType = item.ContentType;
                                md.DocumentPath = item.FileName;
                            }
                            _CiPlatformContext.MissionDocuments.Add(md);
                            _CiPlatformContext.SaveChanges();
                        }
                    }


                    return true;
                }
                else
                {
                    Mission mission = _CiPlatformContext.Missions.FirstOrDefault(x => x.MissionId == obj.mission.MissionId);
                    {
                        mission.Title = obj.mission.Title;
                        mission.Description = obj.mission.Description;
                        mission.ShortDescription = obj.mission.ShortDescription;
                        mission.CountryId = obj.mission.CountryId;
                        mission.CityId = obj.mission.CityId;
                        mission.OrganizationName = obj.mission.OrganizationName;
                        mission.OrganizationDetail = obj.mission.OrganizationDetail;
                        mission.StartDate = obj.mission.StartDate;
                        mission.EndDate = obj.mission.EndDate;
                        mission.MissionType = obj.mission.MissionType;
                        mission.Avaibility = obj.mission.Avaibility;
                        mission.ThemeId = obj.mission.ThemeId;
                        mission.UpdatedAt = DateTime.Now;
                    }
                    _CiPlatformContext.Missions.Update(mission);
                    _CiPlatformContext.SaveChanges();

                    if (obj.editmissionSkills != null)
                    {
                        if (obj.editmissionSkills.Count != 0)
                        {
                            List<MissionSkill> skillList = _CiPlatformContext.MissionSkills.Where(x => x.MissionId == mission.MissionId).ToList();
                            _CiPlatformContext.MissionSkills.RemoveRange(skillList);
                            foreach (var item in obj.editmissionSkills)
                            {
                                MissionSkill ms = new MissionSkill();
                                {
                                    ms.SkillId = item;
                                    ms.MissionId = mission.MissionId;
                                }
                                _CiPlatformContext.MissionSkills.Add(ms);
                                _CiPlatformContext.SaveChanges();
                            }
                        }
                    }

                    List<MissionMedium> missionMedia = _CiPlatformContext.MissionMedia.Where(x => x.MissionId == mission.MissionId && x.MediaType == "png").ToList();
                    _CiPlatformContext.MissionMedia.RemoveRange(missionMedia);
                    if (obj.missionDocuments != null)
                    {
                        foreach (var item in obj.missionDocuments)
                        {
                            MissionMedium mm = new MissionMedium();
                            {
                                mm.MediaName = item.Name;
                                mm.MissionId = mission.MissionId;
                                mm.MediaPath = item.FileName;
                                mm.MediaType = "png";
                            }
                            _CiPlatformContext.MissionMedia.Add(mm);
                            _CiPlatformContext.SaveChanges();
                        }
                    }

                    if (obj.url != null)
                    {
                        MissionMedium mds = _CiPlatformContext.MissionMedia.FirstOrDefault(x => x.MissionId == mission.MissionId && x.MediaType == "url");
                        if (mds != null)
                        {
                            _CiPlatformContext.MissionMedia.Remove(mds);
                        }
                        MissionMedium nmds = new MissionMedium();
                        {
                            nmds.MediaName = obj.url;
                            nmds.MissionId = mission.MissionId;
                            nmds.MediaPath = obj.url;
                            nmds.MediaType = "url";
                        }
                        _CiPlatformContext.MissionMedia.Add(nmds);
                        _CiPlatformContext.SaveChanges();
                    }
                    if (obj.missionDs != null)
                    {
                        List<MissionDocument> missionDocuments = _CiPlatformContext.MissionDocuments.Where(x => x.MissionId == mission.MissionId).ToList();
                        _CiPlatformContext.MissionDocuments.RemoveRange(missionDocuments);

                        if (obj.missionDs != null)
                        {


                            foreach (var item in obj.missionDs)
                            {
                                MissionDocument md = new MissionDocument();
                                {
                                    md.DocumentName = item.FileName;
                                    md.MissionId = mission.MissionId;
                                    md.DocumentType = item.ContentType;
                                    md.DocumentPath = item.FileName;
                                }
                                _CiPlatformContext.MissionDocuments.Add(md);
                                _CiPlatformContext.SaveChanges();
                            }
                        }
                    }



                    return false;
                }
            }
            if (command == 4)
            {
                if (obj.missionTheme.MissionThemeId == 0)
                {
                    MissionTheme missionTheme = new MissionTheme();
                    {
                        missionTheme.Title = obj.missionTheme.Title;

                        missionTheme.Status = obj.missionTheme.Status;
                    }
                    _CiPlatformContext.Add(missionTheme);
                    _CiPlatformContext.SaveChanges();
                    return true;
                }
                else
                {
                    MissionTheme missionTheme = _CiPlatformContext.MissionThemes.FirstOrDefault(x => x.MissionThemeId == obj.missionTheme.MissionThemeId);
                    {
                        missionTheme.Title = obj.missionTheme.Title;

                        missionTheme.Status = obj.missionTheme.Status;
                        missionTheme.UpdatedAt = DateTime.Now;
                    }
                    _CiPlatformContext.Update(missionTheme);
                    _CiPlatformContext.SaveChanges();
                    return false;
                }

            }
            if (command == 5)
            {
                if (obj.skill.SkillId == 0)
                {
                    Skill skill = new Skill();
                    {
                        skill.SkillName = obj.skill.SkillName;

                        skill.Status = obj.skill.Status;
                    }
                    _CiPlatformContext.Add(skill);
                    _CiPlatformContext.SaveChanges();
                    return true;
                }
                else
                {
                    Skill skill = _CiPlatformContext.Skills.FirstOrDefault(x => x.SkillId == obj.skill.SkillId);
                    {
                        skill.SkillName = obj.skill.SkillName;

                        skill.Status = obj.skill.Status;
                        skill.UpdatedAt = DateTime.Now;
                    }
                    _CiPlatformContext.Update(skill);
                    _CiPlatformContext.SaveChanges();
                    return false;
                }

            }
            if (command == 8)
            {
                if (obj.banner.BannerId == 0)
                {

                    Banner banner1 = _CiPlatformContext.Banners.FirstOrDefault(x => x.SortOrder == obj.banner.SortOrder && x.DeletedAt == null);
                    if (banner1 != null)
                    {
                        {
                            banner1.DeletedAt = DateTime.Now;
                        }
                        _CiPlatformContext.Banners.Update(banner1);
                        _CiPlatformContext.SaveChanges();
                    }


                    Banner banner = new Banner();
                    {
                        banner.Text = obj.banner.Text;
                        banner.SortOrder = obj.banner.SortOrder;
                        banner.Image = obj.banner.Image;
                    }
                    _CiPlatformContext.Add(banner);
                    _CiPlatformContext.SaveChanges();
                    return true;
                }
                else
                {
                    Banner banner = _CiPlatformContext.Banners.FirstOrDefault(x => x.BannerId == obj.banner.BannerId);
                    {
                        banner.Text = obj.banner.Text;
                        banner.SortOrder = obj.banner.SortOrder;
                        banner.Image = obj.banner.Image;
                    }
                    _CiPlatformContext.Update(banner);
                    _CiPlatformContext.SaveChanges();
                    return false;
                }
            }

            return true;


        }
        public AdminViewModel EditForm(int id, string page)
        {
            AdminViewModel am = new AdminViewModel();
            {
                if (page == "nav-user")
                {
                    am.user = _CiPlatformContext.Users.FirstOrDefault(x => x.UserId == id);
                }
                if (page == "nav-cms")
                {
                    am.CmsPage = _CiPlatformContext.CmsPages.FirstOrDefault(x => x.CmsPageId == id);
                }

                if (page == "nav-mission")
                {
                    am.mission = _CiPlatformContext.Missions.Include(x => x.GoalMissions).Include(x => x.MissionSkills).Include(x => x.MissionMedia).FirstOrDefault(x => x.MissionId == id);
                }
                if (page == "nav-theme")
                {
                    am.missionTheme = _CiPlatformContext.MissionThemes.FirstOrDefault(x => x.MissionThemeId == id);
                }
                if (page == "nav-skill")
                {
                    am.skill = _CiPlatformContext.Skills.FirstOrDefault(x => x.SkillId == id);
                }
                if (page == "nav-banner")
                {
                    am.banner = _CiPlatformContext.Banners.FirstOrDefault(x => x.BannerId == id);
                }
            }
            return am;
        }
        public bool deleteactivity(int id, int page)
        {
            if (id != 0)
            {
                if (page == 1)
                {
                    User user = _CiPlatformContext.Users.FirstOrDefault(x => x.UserId == id);
                    user.DeletedAt = DateTime.Now;
                    _CiPlatformContext.Users.Update(user);
                    _CiPlatformContext.SaveChanges();
                }
                if (page == 2)
                {
                    CmsPage cms = _CiPlatformContext.CmsPages.FirstOrDefault(x => x.CmsPageId == id);
                    cms.DeletedAt = DateTime.Now;
                    _CiPlatformContext.CmsPages.Update(cms);
                    _CiPlatformContext.SaveChanges();
                }
                if (page == 3)
                {
                    Mission missions = _CiPlatformContext.Missions.FirstOrDefault(x => x.MissionId == id);
                    missions.DeletedAt = DateTime.Now;
                    _CiPlatformContext.Missions.Update(missions);
                    _CiPlatformContext.SaveChanges();
                }
                if (page == 4)
                {
                    MissionTheme theme = _CiPlatformContext.MissionThemes.FirstOrDefault(x => x.MissionThemeId == id);
                    theme.DeletedAt = DateTime.Now;
                    _CiPlatformContext.MissionThemes.Update(theme);
                    _CiPlatformContext.SaveChanges();
                }
                if (page == 5)
                {
                    Skill skill = _CiPlatformContext.Skills.FirstOrDefault(x => x.SkillId == id);
                    skill.DeletedAt = DateTime.Now;
                    _CiPlatformContext.Skills.Update(skill);
                    _CiPlatformContext.SaveChanges();
                }
                if (page == 6)
                {
                    Story story = _CiPlatformContext.Stories.FirstOrDefault(x => x.StoryId == id);
                    story.DeletedAt = DateTime.Now;
                    _CiPlatformContext.Stories.Update(story);
                    _CiPlatformContext.SaveChanges();
                }

                if (page == 8)
                {
                    Banner banner = _CiPlatformContext.Banners.FirstOrDefault(x => x.BannerId == id);
                    banner.DeletedAt = DateTime.Now;
                    _CiPlatformContext.Banners.Update(banner);
                    _CiPlatformContext.SaveChanges();
                }

                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Approval(int id, int page, int status)
        {
            if (id != 0)
            {
                if (page == 6)
                {
                    if (status == 1)
                    {
                        MissionApplication ma = _CiPlatformContext.MissionApplications.Include(x => x.Mission).FirstOrDefault(x => x.MissionApplicationId == id);
                        NotificationSetting check = _CiPlatformContext.NotificationSettings.FirstOrDefault(x => x.UserId == ma.UserId);
                        ma.ApprovalStatus = "Approve";
                        _CiPlatformContext.MissionApplications.Update(ma);
                        _CiPlatformContext.SaveChanges();
                        if (check.MissionApplication == true)
                        {
                            NotificationMessage nm = new NotificationMessage();
                            {
                                nm.UserId = ma.UserId;
                                nm.Message = "Your mission application has been approved : " + ma.Mission.Title;
                                nm.Type = "MissionApplication";
                                nm.Id = ma.MissionId;
                            }
                            _CiPlatformContext.NotificationMessages.Add(nm);
                            _CiPlatformContext.SaveChanges();
                        }
                        return true;
                    }
                    if (status == 0)
                    {
                        MissionApplication ma = _CiPlatformContext.MissionApplications.Include(x => x.Mission).FirstOrDefault(x => x.MissionApplicationId == id);
                        NotificationSetting check = _CiPlatformContext.NotificationSettings.FirstOrDefault(x => x.UserId == ma.UserId);
                        ma.ApprovalStatus = "Decline";
                        _CiPlatformContext.MissionApplications.Update(ma);
                        _CiPlatformContext.SaveChanges();
                        if (check.MissionApplication == true)
                        {
                            NotificationMessage nm = new NotificationMessage();
                            {
                                nm.UserId = ma.UserId;
                                nm.Message = "Your mission application has been rejected : " + ma.Mission.Title;
                                nm.Type = "MissionApplication";
                                nm.Id = ma.MissionId;
                            }
                            _CiPlatformContext.NotificationMessages.Add(nm);
                            _CiPlatformContext.SaveChanges();
                        }
                        return false;
                    }
                }
                if (page == 7)
                {
                    if (status == 1)
                    {
                        Story story = _CiPlatformContext.Stories.FirstOrDefault(x => x.StoryId == id);
                        NotificationSetting check = _CiPlatformContext.NotificationSettings.FirstOrDefault(x => x.UserId == story.UserId);
                        story.Status = "PUBLISHED";
                        _CiPlatformContext.Stories.Update(story);
                        _CiPlatformContext.SaveChanges();
                        if (check.MissionApplication == true)
                        {
                            NotificationMessage nm = new NotificationMessage();
                            {
                                nm.UserId = story.UserId;
                                nm.Message = "Your story  has been approved : " + story.Title;
                                nm.Type = "Story";
                                nm.Id = story.StoryId;
                            }
                            _CiPlatformContext.NotificationMessages.Add(nm);
                            _CiPlatformContext.SaveChanges();
                        }
                        return true;
                    }
                    if (status == 0)
                    {
                        Story story = _CiPlatformContext.Stories.FirstOrDefault(x => x.StoryId == id);
                        NotificationSetting check = _CiPlatformContext.NotificationSettings.FirstOrDefault(x => x.UserId == story.UserId);
                        story.Status = "DECLINED";
                        _CiPlatformContext.Stories.Update(story);
                        _CiPlatformContext.SaveChanges();
                        if (check.MissionApplication == true)
                        {
                            NotificationMessage nm = new NotificationMessage();
                            {
                                nm.UserId = story.UserId;
                                nm.Message = "Your story has been rejected : " + story.Title;
                                nm.Type = "Story";
                                nm.Id = story.StoryId;
                            }
                            _CiPlatformContext.NotificationMessages.Add(nm);
                            _CiPlatformContext.SaveChanges();
                        }
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
