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
            um.cmspages= _CiPlatformContext.CmsPages.Where( x=> x.DeletedAt == null).ToList();
            um.missions = _CiPlatformContext.Missions.Where(x => x.DeletedAt == null).ToList();
            um.missionthemes= _CiPlatformContext.MissionThemes.ToList();
            um.missionSkills = _CiPlatformContext.MissionSkills.Include(x => x.Mission).Include(x => x.Skill).Where(x => x.DeletedAt == null).ToList();
            um.missionapplications = _CiPlatformContext.MissionApplications.Include(x => x.Mission).Include(x => x.User).Where(x => x.ApprovalStatus == "Pending").ToList();

            um.stories=_CiPlatformContext.Stories.Where(x => x.Status == "PENDING" || x.Status == "DRAFT").ToList();




            //um.users = _CiPlatformContext.Users.Where(x => x.DeletedAt == null).ToList();
            //um.missions = _CiPlatformContext.Missions.Where(x => x.DeletedAt == null).ToList();
            //um.CmsPages = _CiPlatformContext.CmsPages.Where(x => x.DeletedAt == null).ToList();
            //um.missionapplications = _CiPlatformContext.MissionApplications.Include(x => x.Mission).Include(x => x.User).Where(x => x.ApprovalStatus == "Pending").ToList();
            //um.stories = _CiPlatformContext.Stories.Include(x => x.User).Where(x => x.Status == "PENDING" || x.Status == "DRAFT").Where(x => x.DeletedAt == null).ToList();
            //um.missionSkills = _CiPlatformContext.MissionSkills.Include(x => x.Mission).Include(x => x.Skill).Where(x => x.DeletedAt == null).ToList();
            //um.missionThemes = _CiPlatformContext.MissionThemes.Where(x => x.DeletedAt == null).ToList();
            return um;
        }
        public AdminViewModel Usersearch(string search, int pg)
        {
            var pageSize = 5;
            AdminViewModel obj=getData();




            if (search != null)
            {
                search = search.ToLower();
                obj.users = obj.users.Where(x => x.FirstName.ToLower().Contains(search)).ToList();
                obj.cmspages = obj.cmspages.Where(x=>x.Title.ToLower().Contains(search)).ToList();
                obj.missions=obj.missions.Where(x=>x.Title.ToLower().Contains(search) || x.MissionType.ToLower().Contains(search)).ToList();
                obj.missionthemes = obj.missionthemes.Where(x => x.Title.ToLower().Contains(search)).ToList();
                obj.missionSkills=obj.missionSkills.Where(x=>x.Skill.SkillName.ToLower().Contains(search)).ToList();
                obj.missionapplications=obj.missionapplications.Where(x=>x.Mission.Title.ToLower().Contains(search)).ToList();
                obj.stories=obj.stories.Where(x=>x.Title.ToLower().Contains(search)).ToList();
            
            }
            if (pg != 0)
            {
                obj.users = obj.users.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
                obj.cmspages = obj.cmspages.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
                obj.missions = obj.missions.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
                obj.missionthemes=obj.missionthemes.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
                obj.missionSkills = obj.missionSkills.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
                obj.missionapplications = obj.missionapplications.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
                obj.stories = obj.stories.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
            }
            return obj;


        }

        public bool addcms(AdminViewModel obj, int command)
        {
            CmsPage cp=new CmsPage();
            {
                cp.Title = obj.CmsPage.Title;
                cp.Description = obj.CmsPage.Description;
                cp.Slug = obj.CmsPage.Slug; 
            }
            _CiPlatformContext.Add(cp);
            _CiPlatformContext.SaveChanges();
          return true;
        }
        public bool deleteactivity(int id,int page)
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
                        MissionApplication ma = _CiPlatformContext.MissionApplications.FirstOrDefault(x => x.MissionApplicationId == id);
                        ma.ApprovalStatus = "Approve";
                        _CiPlatformContext.MissionApplications.Update(ma);
                        _CiPlatformContext.SaveChanges();
                        return true;
                    }
                    if (status == 0)
                    {
                        MissionApplication ma = _CiPlatformContext.MissionApplications.FirstOrDefault(x => x.MissionApplicationId == id);
                        ma.ApprovalStatus = "Decline";
                        _CiPlatformContext.MissionApplications.Update(ma);
                        _CiPlatformContext.SaveChanges();
                        return false;
                    }
                }
                if (page == 7)
                {
                    if (status == 1)
                    {
                        Story story = _CiPlatformContext.Stories.FirstOrDefault(x => x.StoryId == id);
                        story.Status = "PUBLISHED";
                        _CiPlatformContext.Stories.Update(story);
                        _CiPlatformContext.SaveChanges();
                        return true;
                    }
                    if (status == 0)
                    {
                        Story story = _CiPlatformContext.Stories.FirstOrDefault(x => x.StoryId == id);
                        story.Status = "DECLINED";
                        _CiPlatformContext.Stories.Update(story);
                        _CiPlatformContext.SaveChanges();
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
