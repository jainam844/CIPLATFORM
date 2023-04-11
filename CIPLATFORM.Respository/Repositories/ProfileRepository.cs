using CIPLATFORM.Entities.Data;
using CIPLATFORM.Entities.Models;
using CIPLATFORM.Entities.ViewModels;
using CIPLATFORM.Respository.Interface;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Security;

using MimeKit.Text;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using Microsoft.EntityFrameworkCore;

namespace CIPLATFORM.Respository.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        public readonly CiPlatformContext _CiPlatformContext;

        public ProfileRepository(CiPlatformContext CiPlatformContext)
        {
            _CiPlatformContext = CiPlatformContext;
        }


        public ProfileViewModel getUser(int UId)
        {
            User user = _CiPlatformContext.Users.FirstOrDefault(x => x.UserId == UId);
            List<UserSkill> us = _CiPlatformContext.UserSkills.Include(m => m.Skill).Where(m => m.UserId == UId).ToList();
            List<Skill> skills = _CiPlatformContext.Skills.Include(m => m.UserSkills).ToList();

            foreach (var userSkill in us)
            {
                skills = skills.Where(skill => skill.SkillId != userSkill.SkillId).ToList();
            }

            ProfileViewModel pm = new ProfileViewModel();
            if (user != null)
            {
                {
                    pm.UserId = user.UserId;
                    pm.FirstName = user.FirstName;
                    pm.LastName = user.LastName;
                    pm.Avatar = user.Avatar;
                    pm.EmployeeId = user.EmployeeId;
                    pm.Title = user.Title;
                    pm.Department = user.Department;
                    pm.ProfileText = user.ProfileText;
                    pm.Department = user.Department;
                    pm.WhyIVolunteer = user.WhyIVolunteer;
                    pm.CountryId = user.CountryId;
                    pm.CityId = user.CityId;
                    pm.LinkedInUrl = user.LinkedInUrl;
                    pm.userSkills = us;
                    pm.skills = skills;
                }
            }
            return pm;
        }
        public bool changepassword(ProfileViewModel user, int UId)
        {
            User pm = _CiPlatformContext.Users.FirstOrDefault(x => x.UserId == UId);
            if (pm != null)
            {
                if (user.resetPass.OldPassword == pm.Password)
                {
                    {
                        pm.Password = user.resetPass.Password;

                    }
                    _CiPlatformContext.Users.Update(pm);
                    _CiPlatformContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
        public bool saveProfile(ProfileViewModel user, int UId)
        {

            User pm = _CiPlatformContext.Users.FirstOrDefault(x => x.UserId == UId);



            if (pm != null)
            {
                {
                    pm.FirstName = user.FirstName;
                    pm.LastName = user.LastName;

                    if (user.Avatarfile != null)
                    {
                        pm.Avatar = user.Avatarfile.FileName;
                    }
                    pm.EmployeeId = user.EmployeeId;
                    pm.Title = user.Title;
                    pm.Department = user.Department;
                    pm.ProfileText = user.ProfileText;
                    pm.Department = user.Department;
                    pm.WhyIVolunteer = user.WhyIVolunteer;
                    pm.CountryId = user.CountryId;
                    pm.CityId = user.CityId;
                    pm.LinkedInUrl = user.LinkedInUrl;
                }
                _CiPlatformContext.Users.Update(pm);
                _CiPlatformContext.SaveChanges();



                List<UserSkill> us = _CiPlatformContext.UserSkills.Where(x => x.UserId == UId).ToList();
                _CiPlatformContext.UserSkills.RemoveRange(us);

                foreach (var skill in user.skillsToAdd)
                {
                    UserSkill addSkill = new UserSkill();
                    {
                        addSkill.UserId = UId;
                        addSkill.SkillId = skill;
                    }
                    _CiPlatformContext.UserSkills.Add(addSkill);
                    _CiPlatformContext.SaveChanges();
                }
                return true;
            }
            return true;
        }

        public bool ContactUs(ProfileViewModel obj)
        {

            #region Send Mail
            var mailBody = "<h2>I hope this email finds you well. My name is " + obj.contactus.Name + " and I wanted to take a moment to you.</h1>" + "<h1>" + obj.contactus.Message + "</h1><br><h2>" + "</h2>";

            // create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(obj.contactus.Email));
            email.To.Add(MailboxAddress.Parse("jainamshah492@gmail.com"));
            email.Subject = obj.contactus.subject;
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

        public List<MissionApplication> TimeMission(int UId)
        {
            List<MissionApplication> missions = _CiPlatformContext.MissionApplications.Include(m => m.Mission).Where(x => x.UserId == UId && x.ApprovalStatus == "Approve" && x.Mission.MissionType == "Time").ToList();
            return missions;
        }
        public List<MissionApplication> GoalMission(int UId)
        {
            List<MissionApplication> missions = _CiPlatformContext.MissionApplications.Include(m => m.Mission).Where(x => x.UserId == UId && x.ApprovalStatus == "Approve" && x.Mission.MissionType == "Goal").ToList();
            return missions;
        }
        public List<Timesheet> timesheet(int UId)
        {
            List<Timesheet> tts = _CiPlatformContext.Timesheets.Include(m => m.Mission).Where(x => x.UserId == UId && x.Status == "APPROVED" && x.Mission.MissionType == "Time").ToList();
            return tts;
        }
        public List<Timesheet> goaltimesheet(int UId)
        {
            List<Timesheet> gts = _CiPlatformContext.Timesheets.Include(m => m.Mission).Where(x => x.UserId == UId && x.Status == "APPROVED" && x.Mission.MissionType == "Goal").ToList();
            return gts;
        }
        public ProfileViewModel GetTimsheet(int UId)
        {
            List<MissionApplication> tm = TimeMission(UId);
            List<MissionApplication> gm = GoalMission(UId);
            List<Timesheet> ts = timesheet(UId);
            List<Timesheet> gs = goaltimesheet(UId);
            ProfileViewModel Timemodel = new ProfileViewModel();
            {
                Timemodel.timemissions = tm;
                Timemodel.goalmissions = gm;
                Timemodel.timesheets = ts;
                Timemodel.goaltimesheets = gs;
            }
            return Timemodel;

        }

    }
}
