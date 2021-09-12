using AttendenceBackEnd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendenceBackEnd.Data
{
    public class ApiDbContext : IdentityDbContext<AppUser,IdentityRole<int>,int>
    {
        public ApiDbContext(DbContextOptions options) : base(options)
        {            
        }
        public DbSet<Attendance> Attendance { get; set; }

        //private void ApiDbContext_SaveChangesFailed(object sender, SaveChangesFailedEventArgs e)
        //{
        //    Console.WriteLine(e.Exception.Message);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seeding a  'Administrator' role to AspNetRoles table
            modelBuilder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int> {
                Id = 1,
                Name = "Admin", NormalizedName = "Admin".ToUpper() 
            },
            new IdentityRole<int>
            {
                Id = 2,
                Name = "User",
                NormalizedName = "User".ToUpper()
            }
            );


            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<IdentityUser>();


            //Seeding the User to AspNetUsers table
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = 1,
                    FullName = "Mohamed Hassan",
                    UserName = "hassan",
                    NormalizedUserName = "HASSAN".ToUpper(),
                    NormalizedEmail = "MOHAMED.HASSAN.HIGAZY@OUTLOOK.COM".ToUpper(),
                    Email = "mohamed.hassan.higazy@outlook.com",
                    EmailConfirmed = true,
                    Checked = 1,
                    LockoutEnabled = true,
                    PasswordHash = hasher.HashPassword(null, "123456"),
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                },
                new AppUser
                {
                    Id = 2,
                    FullName = "Alaa",
                    NormalizedEmail = "ALAA@GMAIL.COM".ToUpper(),
                    Email = "alaa@gmail.com",
                    EmailConfirmed = true,
                    UserName = "alaa",
                    NormalizedUserName = "ALAA".ToUpper(),
                    Checked = 1,
                    LockoutEnabled = true,
                    PasswordHash = hasher.HashPassword(null, "123456"),
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                },
                new AppUser
                {
                    Id = 3,
                    FullName = "Mohamed Ali",
                    NormalizedEmail = "MOHAMEDALI@OUTLOOK.COM".ToUpper(),
                    Email = "mohamedali@outlook.com",
                    EmailConfirmed = true,
                    UserName = "ali",
                    NormalizedUserName = "ALI".ToUpper(),
                    Checked = 1,
                    LockoutEnabled = true,
                    PasswordHash = hasher.HashPassword(null, "123456"),
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                }
            );


            //Seeding the relation between our user and role to AspNetUserRoles table
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>
                {
                    RoleId = 1,
                    UserId = 1
                }, new IdentityUserRole<int>
                {
                    RoleId = 2,
                    UserId = 2
                }, new IdentityUserRole<int>
                {
                    RoleId = 2,
                    UserId = 3
                }
            );

            modelBuilder.Entity<Attendance>().HasData(
                new Attendance
                {
                    Id=1,
                    AppUserId = 1,
                    TotalTime = 8,
                    AttendanceDate = Convert.ToDateTime("05/09/2021")
                },
                new Attendance
                {
                    Id = 2,
                    AppUserId = 1,
                    TotalTime = 9,
                    AttendanceDate = Convert.ToDateTime("06/09/2021")
                }, new Attendance
                {
                    Id = 3,
                    AppUserId = 1,
                    TotalTime = 8,
                    AttendanceDate = Convert.ToDateTime("07/09/2021")
                }, new Attendance
                {
                    Id = 4,
                    AppUserId = 1,
                    TotalTime = 10,
                    AttendanceDate = Convert.ToDateTime("08/09/2021")
                }, new Attendance
                {
                    Id = 5,
                    AppUserId = 1,
                    TotalTime = 8,
                    AttendanceDate = Convert.ToDateTime("09/09/2021")
                }, new Attendance
                {
                    Id = 6,
                    AppUserId = 1,
                    TotalTime = 8,
                    AttendanceDate = Convert.ToDateTime("11/09/2021")
                }, new Attendance
                {
                    Id = 7,
                    AppUserId = 1,
                    TotalTime = 9,
                    AttendanceDate = Convert.ToDateTime("12/09/2021")
                }, new Attendance
                {
                    Id = 8,
                    AppUserId = 2,
                    TotalTime = 6,
                    AttendanceDate = Convert.ToDateTime("05/09/2021")
                },
                    new Attendance
                    {
                        Id = 9,
                        AppUserId = 2,
                        TotalTime = 10,
                        AttendanceDate = Convert.ToDateTime("06/09/2021")
                    }, new Attendance
                    {
                        Id = 10,
                        AppUserId = 2,
                        TotalTime = 7,
                        AttendanceDate = Convert.ToDateTime("07/09/2021")
                    }, new Attendance
                    {
                        Id = 11,
                        AppUserId = 2,
                        TotalTime = 9,
                        AttendanceDate = Convert.ToDateTime("08/09/2021")
                    }, new Attendance
                    {
                        Id = 12,
                        AppUserId = 2,
                        TotalTime = 8,
                        AttendanceDate = Convert.ToDateTime("09/09/2021")
                    }, new Attendance
                    {
                        Id = 13,
                        AppUserId = 2,
                        TotalTime = 8,
                        AttendanceDate = Convert.ToDateTime("12/09/2021")
                    }, new Attendance
                    {
                        Id = 14,
                        AppUserId = 3,
                        TotalTime = 6,
                        AttendanceDate = Convert.ToDateTime("05/09/2021")
                    },
                    new Attendance
                    {
                        Id = 15,
                        AppUserId = 3,
                        TotalTime = 10,
                        AttendanceDate = Convert.ToDateTime("06/09/2021")
                    }, new Attendance
                    {
                        Id = 16,
                        AppUserId = 3,
                        TotalTime = 8,
                        AttendanceDate = Convert.ToDateTime("07/09/2021")
                    }, new Attendance
                    {
                        Id = 17,
                        AppUserId = 3,
                        TotalTime = 8,
                        AttendanceDate = Convert.ToDateTime("09/09/2021")
                    }, new Attendance
                    {
                        Id = 18,
                        AppUserId = 3,
                        TotalTime = 7,
                        AttendanceDate = Convert.ToDateTime("11/09/2021")
                    }, new Attendance
                    {
                        Id = 19,
                        AppUserId = 3,
                        TotalTime = 9,
                        AttendanceDate = Convert.ToDateTime("12/09/2021")
                    }
            );
        }
    }
}
