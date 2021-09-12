using AttendenceBackEnd.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendenceBackEnd.Data
{
    public class AppDbInitializer
    {
        public static void Seed (IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApiDbContext>();


                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(new Microsoft.AspNetCore.Identity.IdentityRole<int>
                    {
                        Name = "Admin",
                        NormalizedName = "ADMIN"                        
                    },
                    new Microsoft.AspNetCore.Identity.IdentityRole<int>
                    {
                        Name = "User",
                        NormalizedName = "USER"
                    }
                    );
                    context.SaveChanges();
                }
                var hasher = new PasswordHasher<IdentityUser>();
                    
                if (!context.Users.Any())
                {
                    context.Users.AddRange(new AppUser
                    {
                        FullName = "Mohamed Hassan",
                        UserName = "hassan",
                        NormalizedUserName = "HASSAN",
                        NormalizedEmail = "MOHAMED.HASSAN.HIGAZY@OUTLOOK.COM",
                        Email = "mohamed.hassan.higazy@outlook.com",
                        EmailConfirmed = true,
                        Checked = 1,
                        LockoutEnabled = true,
                        PasswordHash = hasher.HashPassword(null, "123456"),
                        SecurityStamp = string.Empty
                    },
                    new AppUser
                    {
                        FullName = "Alaa",
                        NormalizedEmail = "ALAA@GMAIL.COM",
                        Email = "alaa@gmail.com",
                        EmailConfirmed = true,
                        UserName = "alaa",
                        NormalizedUserName = "ALAA",
                        Checked = 1,
                        LockoutEnabled = true,
                        PasswordHash = hasher.HashPassword(null, "123456"),
                        SecurityStamp = string.Empty
                    },
                     new AppUser
                     {
                         FullName = "Mohamed Ali",
                         NormalizedEmail = "MOHAMEDALI@OUTLOOK.COM".ToUpper(),
                         Email = "mohamedali@outlook.com",
                         EmailConfirmed = true,
                         UserName = "ali",
                         NormalizedUserName = "ALI".ToUpper(),
                         Checked = 1,
                         LockoutEnabled = true,
                         PasswordHash = hasher.HashPassword(null, "123456"),
                         SecurityStamp = string.Empty
                     }
                    );
                    context.SaveChanges();

                }
                //PasswordHasher<AppUser> passwordHasher = new PasswordHasher<AppUser>();
                //passwordHasher.HashPassword(context.Users.Where(x=> x.Id ==1).FirstOrDefault(), "123456");
                //passwordHasher.HashPassword(context.Users.Where(x=> x.Id ==2).FirstOrDefault(), "123456");
                //passwordHasher.HashPassword(context.Users.Where(x=> x.Id ==3).FirstOrDefault(), "123456");


                if (!context.UserRoles.Any())
                {
                    context.UserRoles.AddRange(new Microsoft.AspNetCore.Identity.IdentityUserRole<int>
                    {
                        UserId = 1,
                        RoleId = 1
                    },
                    new Microsoft.AspNetCore.Identity.IdentityUserRole<int>
                    {
                        UserId = 2,
                        RoleId = 2
                    },
                    new Microsoft.AspNetCore.Identity.IdentityUserRole<int>
                    {
                        UserId = 3,
                        RoleId = 2
                    }
                    );
                    context.SaveChanges();
                }

                if (!context.Attendance.Any())
                {
                    context.Attendance.AddRange(new Attendance
                    {
                        AppUserId = 1,
                        TotalTime = 8,
                        AttendanceDate = Convert.ToDateTime("05/09/2021")
                    },
                new Attendance
                {
                    AppUserId = 1,
                    TotalTime = 9,
                    AttendanceDate = Convert.ToDateTime("06/09/2021")
                }, new Attendance
                {
                    AppUserId = 1,
                    TotalTime = 8,
                    AttendanceDate = Convert.ToDateTime("07/09/2021")
                }, new Attendance
                {
                    AppUserId = 1,
                    TotalTime = 10,
                    AttendanceDate = Convert.ToDateTime("08/09/2021")
                }, new Attendance
                {
                    AppUserId = 1,
                    TotalTime = 8,
                    AttendanceDate = Convert.ToDateTime("09/09/2021")
                }, new Attendance
                {
                    AppUserId = 1,
                    TotalTime = 8,
                    AttendanceDate = Convert.ToDateTime("11/09/2021")
                }, new Attendance
                {
                    AppUserId = 1,
                    TotalTime = 9,
                    AttendanceDate = Convert.ToDateTime("12/09/2021")
                }, new Attendance
                {
                    AppUserId = 2,
                    TotalTime = 6,
                    AttendanceDate = Convert.ToDateTime("05/09/2021")
                },
                    new Attendance
                    {
                        AppUserId = 2,
                        TotalTime = 10,
                        AttendanceDate = Convert.ToDateTime("06/09/2021")
                    }, new Attendance
                    {
                        AppUserId = 2,
                        TotalTime = 7,
                        AttendanceDate = Convert.ToDateTime("07/09/2021")
                    }, new Attendance
                    {
                        AppUserId = 2,
                        TotalTime = 9,
                        AttendanceDate = Convert.ToDateTime("08/09/2021")
                    }, new Attendance
                    {
                        AppUserId = 2,
                        TotalTime = 8,
                        AttendanceDate = Convert.ToDateTime("09/09/2021")
                    }, new Attendance
                    {
                        AppUserId = 2,
                        TotalTime = 8,
                        AttendanceDate = Convert.ToDateTime("12/09/2021")
                    }, new Attendance
                    {
                        AppUserId = 3,
                        TotalTime = 6,
                        AttendanceDate = Convert.ToDateTime("05/09/2021")
                    },
                    new Attendance
                    {
                        AppUserId = 3,
                        TotalTime = 10,
                        AttendanceDate = Convert.ToDateTime("06/09/2021")
                    }, new Attendance
                    {
                        AppUserId = 3,
                        TotalTime = 8,
                        AttendanceDate = Convert.ToDateTime("07/09/2021")
                    }, new Attendance
                    {
                        AppUserId = 3,
                        TotalTime = 8,
                        AttendanceDate = Convert.ToDateTime("09/09/2021")
                    }, new Attendance
                    {
                        AppUserId = 3,
                        TotalTime = 7,
                        AttendanceDate = Convert.ToDateTime("11/09/2021")
                    }, new Attendance
                    {
                        AppUserId = 3,
                        TotalTime = 9,
                        AttendanceDate = Convert.ToDateTime("12/09/2021")
                    }
                    );
                    context.SaveChanges();

                }


                //context.SaveChanges();
            }
        }
    }
}
