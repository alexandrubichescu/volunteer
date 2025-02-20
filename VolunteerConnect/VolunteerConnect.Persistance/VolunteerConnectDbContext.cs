using VolunteerConnect.Application.Contracts;
using VolunteerConnect.Domain.Common;
using VolunteerConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace VolunteerConnect.Persistance
{
    public class VolunteerConnectDbContext : DbContext
    {
        private readonly ILoggedInUserService _loggedInUserService;

        public VolunteerConnectDbContext(DbContextOptions<VolunteerConnectDbContext> options, ILoggedInUserService loggedInUserService)
            : base(options)
        {
            _loggedInUserService = loggedInUserService;
        }
        public VolunteerConnectDbContext(DbContextOptions<VolunteerConnectDbContext> options)
           : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ParticipationOrder> ParticipationOrder { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VolunteerConnectDbContext).Assembly);

            // Seed data for Categories
            var concertGuid = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
            var healthGuid = Guid.Parse("{6313179F-7837-473A-A4D5-A5571B43E6A6}");
            var fireGuid = Guid.Parse("{BF3F3002-7E53-441E-8B76-F6280BE284AA}");
            var elderGuid = Guid.Parse("{FE98F549-E790-4E9F-AA16-18C2292A2EE9}");
            var educationGuid = Guid.Parse("{BF3F4102-7E53-441E-8B76-F6280BE284AA}");
            var environmentGuid = Guid.Parse("{BE75F549-E790-4E9F-AA16-18C2292A2EE9}");

            var user1Guid = Guid.Parse("{f2b41e27-0adb-402b-80de-82ed7383a93b}");

            modelBuilder.Entity<Category>().HasData(
    new Category
    {
        CategoryId = concertGuid,
        Title = "Concerts",
        Description = "Experience live music events and connect with others.",
        ImageUrl = "https://ca-times.brightspotcdn.com/dims4/default/dd77f6f/2147483647/strip/true/crop/3000x1997+0+0/resize/1200x799!/quality/75/?url=https%3A%2F%2Fcalifornia-times-brightspot.s3.amazonaws.com%2F83%2F39%2F993dc8cc4f85a898b034c7ee1c15%2Fla-ca-cell-phones-concert-0035.JPG"
    },
    new Category
    {
        CategoryId = healthGuid,
        Title = "HealthCare",
        Description = "Volunteer to support healthcare initiatives.",
        ImageUrl = "https://canadianbusinesscollege.com/wp-content/uploads/2021/03/Mar-12-healthcare-programs-1200x800.jpg"
    },
    new Category
    {
        CategoryId = fireGuid,
        Title = "FireRescue",
        Description = "Assist in fire safety and rescue operations.",
        ImageUrl = "https://static.vecteezy.com/system/resources/previews/033/640/801/non_2x/a-female-firefighter-carrying-a-child-to-safety-during-a-rescue-mission-generative-ai-photo.jpg",
    },
    new Category
    {
        CategoryId = elderGuid,
        Title = "ElderlyCare",
        Description = "Provide care and companionship to the elderly.",
        ImageUrl = "https://homecare-aid.com/wp-content/uploads/2024/04/Why-Elderly-Care-Can-Be-So-Important-1024x609.jpg.webp"
    },
    new Category
    {
        CategoryId = educationGuid,
        Title = "Education",
        Description = "Help educate and mentor underprivileged children.",
        ImageUrl = "https://www.volunteerforever.com/wp-content/uploads/2019/01/Cheap-Affordable-Volunteer-Programs-Header.jpg"
    },
    new Category
    {
        CategoryId = environmentGuid,
        Title = "Environment",
        Description = "Participate in initiatives to protect the environment.",
        ImageUrl = "https://servicesatpar.weebly.com/uploads/1/1/9/2/119226956/environmental-volunteering_orig.jpg"
    }
);


            // Seed data for Events
            var event1Id = Guid.Parse("{EE272F8B-6096-4CB6-8625-BB4BB2D89E8B}");
            var event2Id = Guid.Parse("{3448D5A4-0F72-4DD7-BF15-C14A46B26C00}");
            var event3Id = Guid.Parse("{3998D5A4-0F72-4DD7-BF15-C14A76B26C33}");
            var event4Id = Guid.Parse("{0005D5A4-0F72-4DD7-BF15-C14A66B26C22}");
            var event5Id = Guid.Parse("{6648D5A4-0F72-4DD7-BF15-C14A36B26C11}");

            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    EventId = event1Id,
                    Title = "John Egbert Live",
                    CompanyHolder = "John Egbert",
                    Location = "Cluj Napoca",
                    Date = new DateTime(2025, 06, 25),
                    MaxParticipants = 20,
                    Description = "Join John for his farewell tour.",
                    ImageUrl = "https://example.com/image1.jpg",
                    CategoryId = concertGuid
                },
                new Event
                {
                    EventId = event2Id,
                    Title = "Small fire rescue",
                    CompanyHolder = "Fire State Department",
                    Location = "Cluj Napoca",
                    Date = new DateTime(2025, 07, 25),
                    MaxParticipants = 25,
                    Description = "Helping the fire department.",
                    ImageUrl = "https://example.com/image2.jpg",
                    CategoryId = fireGuid
                },
                new Event
                {
                    EventId = event3Id,
                    Title = "Health Awareness Drive",
                    CompanyHolder = "HealthCare Inc.",
                    Location = "Bucharest",
                    Date = new DateTime(2025, 08, 10),
                    MaxParticipants = 50,
                    Description = "Spreading health awareness.",
                    ImageUrl = "https://example.com/image3.jpg",
                    CategoryId = healthGuid
                },
                new Event
                {
                    EventId = event4Id,
                    Title = "Elderly Assistance Program",
                    CompanyHolder = "CareFirst",
                    Location = "Iasi",
                    Date = new DateTime(2025, 09, 15),
                    MaxParticipants = 30,
                    Description = "Helping the elderly.",
                    ImageUrl = "https://example.com/image4.jpg",
                    CategoryId = elderGuid
                },
                new Event
                {
                    EventId = event5Id,
                    Title = "Music for All",
                    CompanyHolder = "MusicWorld",
                    Location = "Brasov",
                    Date = new DateTime(2025, 10, 05),
                    MaxParticipants = 100,
                    Description = "Music event for all.",
                    ImageUrl = "https://example.com/image5.jpg",
                    CategoryId = concertGuid
                }
            );

            // Seed data for ParticipationOrder
            modelBuilder.Entity<ParticipationOrder>().HasData(
                new ParticipationOrder
                {
                    Id = Guid.Parse("{EE572F8B-6096-4CB6-8625-BB1BB2D89E8B}"),
                    UserId = user1Guid,
                    EventId = event1Id,
                    Status = ParticipationStatus.Pending
                },
                new ParticipationOrder
                {
                    Id = Guid.Parse("{EE262F8B-6096-4CB6-8625-BB4BB2D89E2B}"),
                    UserId = user1Guid,
                    EventId = event2Id,
                    Status = ParticipationStatus.Approved
                }
                
            );
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var userName = _loggedInUserService.UserName;
            var dateNow= DateTime.Now;
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = dateNow;
                        entry.Entity.CreatedBy = userName ?? "System";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = dateNow;
                        entry.Entity.LastModifiedBy = userName ?? "System";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
