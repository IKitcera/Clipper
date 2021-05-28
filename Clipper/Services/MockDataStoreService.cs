using Clipper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clipper.Services
{
    public class MockDataStoreService
    {
        readonly List<Profile> profiles;
        readonly List<User> users;
        readonly List<PhotoPost> posts;
        public List<Profile> Profiles { get => profiles; }
        public List<User> Users { get => users; }
        public MockDataStoreService()
        {

            users = new List<User>() { 

                //Id = Guid.NewGuid().ToString()
            new User { Id ="1111", Name = "User #1", Email = "user1@gmail.com", PasswordHash="" },
                 new User{ Id ="2222", Name = "User #2", Email = "user2@gmail.com", PasswordHash="" },
                  new User{ Id = "3333", Name = "User #3", Email = "user3@gmail.com", PasswordHash="" },
                   new User{ Id = "4444", Name = "User #4", Email = "user4@gmail.com", PasswordHash="" },
            };
            profiles = new List<Profile>()
            {
                new Profile {
                    Id = Guid.NewGuid().ToString(), UserId = users[0].Id,
                    SubscribedId= new List<string>() {users[2].Id, users[1].Id},
                    SubscribersId = new List<string>(){ users[3].Id  },
                    TextAbout = "Hi, I`m user 1",
                    Avatar = "",
                    PhotoPosts = new List<PhotoPost>()
                    {
                        new PhotoPost{Id = Guid.NewGuid().ToString(),
                            UserId ="1111",
                            Images = new List<string>() {"https://cdn.eso.org/images/thumb300y/eso1907a.jpg"},
                            TextBelow =  "photo...1",
                            Comments = new List<Comment>()  {  new Comment{Id = Guid.NewGuid().ToString(), Text = "Whoa", UserID = users[2].Id }  },
                            CreatingTime = DateTime.Now,
                            Reactions = new  List<Reaction>(){ Reaction.Positive }
                        },
                        new PhotoPost {  Id = Guid.NewGuid().ToString(),
                              UserId ="1111",
                             Images = new List<string>()
                            { "https://www.imagescanada.ca/wp-content/uploads/2019/03/Spectacular-Photos-of-Niagara-Falls-Casinos.jpg"     },
                            TextBelow = "#sl",
                            Comments = new List<Comment>()   {new Comment{Id = Guid.NewGuid().ToString(), Text = "well", UserID = users[2].Id }   },
                             CreatingTime = DateTime.Now,
                            Reactions = new List<Reaction>() { Reaction.Positive }
                        },
                        new PhotoPost{Id = Guid.NewGuid().ToString(),
                            UserId ="1111",
                            Images = new List<string>() {"https://upload.wikimedia.org/wikipedia/commons/9/9a/Gull_portrait_ca_usa.jpg"},
                            TextBelow =  "photo..3...",
                            Comments = new List<Comment>()  {  new Comment{Id = Guid.NewGuid().ToString(), Text = "Whoa", UserID = users[2].Id }  },
                            CreatingTime = DateTime.Now,
                            Reactions = new  List<Reaction>(){ Reaction.Positive }
                        },
                        new PhotoPost {  Id = Guid.NewGuid().ToString(),
                              UserId ="1111",
                             Images = new List<string>()
                            { "https://st.depositphotos.com/1428083/2946/i/600/depositphotos_29460297-stock-photo-bird-cage.jpg"     },
                            TextBelow = "#sl2",
                            Comments = new List<Comment>()   {new Comment{Id = Guid.NewGuid().ToString(), Text = "well", UserID = users[2].Id }   },
                             CreatingTime = DateTime.Now,
                            Reactions = new List<Reaction>() { Reaction.Positive }
                        },
                          new PhotoPost {  Id = Guid.NewGuid().ToString(),
                              UserId ="1111",
                             Images = new List<string>()
                            { "https://static-cse.canva.com/blob/140259/ComposeStunningImages7.jpg"     },
                            TextBelow = "photo..6",
                            Comments = new List<Comment>()   {new Comment{Id = Guid.NewGuid().ToString(), Text = "well", UserID = users[2].Id }   },
                             CreatingTime = DateTime.Now,
                            Reactions = new List<Reaction>() { Reaction.Positive }
                        },
                        new PhotoPost {Id = Guid.NewGuid().ToString(),
                        UserId = "1111",
                        Images = new List<string>()
                        {
                            "data:image/jpeg;base64,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"
                        },
                        TextBelow = "wood landscape",
                            Comments = new List<Comment>() ,
                             CreatingTime = DateTime.Now,
                            Reactions = new List<Reaction>() { Reaction.Negative }
                        }
                    }
                },
               new Profile {
                    Id = Guid.NewGuid().ToString(), UserId = users[1].Id,
                    SubscribedId = new List<string>() { users[1].Id },
                    SubscribersId = new List<string>(){ users[3].Id  },
                    TextAbout = "Hi, I`m user 2",
                    Avatar = "https://www.w3schools.com/howto/img_avatar.png",
                    PhotoPosts = new List<PhotoPost>()
                    {
                        new PhotoPost{Id = Guid.NewGuid().ToString(),
                        UserId="2222",
                            Images = new List<string>() { "https://buffer.com/library/content/images/size/w600/2020/09/Frame-47.png",
                            "https://cdn.shopify.com/s/files/1/1734/4153/products/4T1A8680.jpg?v=1616709496",
                            "https://st3.depositphotos.com/12985790/17376/i/1600/depositphotos_173765156-stock-photo-woman-in-leather-boots.jpg"},
                            TextBelow =  "Boots...",
                            Comments = new List<Comment>() ,
                            CreatingTime = DateTime.Now,
                            Reactions = new  List<Reaction>(){ Reaction.Positive }
                        }
                    }
                },
               new Profile {
                    Id = Guid.NewGuid().ToString(), UserId = users[2].Id,
                    SubscribedId= new List<string>() {users[2].Id, users[1].Id},
                    SubscribersId = new List<string>(){ users[3].Id  },
                    TextAbout = "Hi, I`m user 3",
                     Avatar = "https://image.freepik.com/free-vector/profile-icon-male-avatar-hipster-man-wear-headphones_48369-8728.jpg",
                    PhotoPosts = new List<PhotoPost>()
                    {
                        new PhotoPost{Id = Guid.NewGuid().ToString(),
                        UserId = "3333",
                            Images = new List<string>() { "https://images.pexels.com/photos/235986/pexels-photo-235986.jpeg"},
                            TextBelow =  "Wall...",
                            Comments = new List<Comment>()  {  new Comment{Id = Guid.NewGuid().ToString(), Text = "Whoa", UserID = users[2].Id }  },
                            CreatingTime = DateTime.Now,
                            Reactions = new  List<Reaction>(){ Reaction.Positive }
                        },
                        new PhotoPost{Id = Guid.NewGuid().ToString(),
                        UserId = "3333",
                            Images = new List<string>() {"https://images.ctfassets.net/hrltx12pl8hq/pgYGuW2X3wHni2PG3dLV0/80a2008f53a645da1db3c1b5260fd3fd/hero_spring_6.jpg?fit=fill&w=800&h=450"},
                            TextBelow =  "leaves...",
                            Comments = new List<Comment>()  {  new Comment{Id = Guid.NewGuid().ToString(), Text = "Nice", UserID = users[2].Id }  },
                            CreatingTime = DateTime.Now,
                            Reactions = new  List<Reaction>(){ Reaction.Positive, Reaction.Negative }
                        }
                        
                    }
                },new Profile {
                    Id = Guid.NewGuid().ToString(), UserId = users[3].Id,
                    SubscribedId= new List<string>() {users[2].Id, users[1].Id},
                    SubscribersId = new List<string>(){ users[3].Id  },
                    TextAbout = "Hi, I`m user 4",
                    PhotoPosts = new List<PhotoPost>()
                }
            };
            posts = new List<PhotoPost>();
            var postsCompexArr = profiles.Select(p => p.PhotoPosts);
            foreach(var oneD in postsCompexArr)
            {
                foreach(var secondD in oneD)
                {
                    posts.Add(secondD);
                }
            }
        }

    }
}