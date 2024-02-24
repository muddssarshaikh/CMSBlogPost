# CMSBlogPost
.Net core backend Api's for categories Blogs posts with comments 
Implement RESTful API endpoints for CRUD operations on:

Categories:

create .net core api with below endpoints with sql tables listed 

CREATE TABLE Categories (
    CategoryId INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(255) NOT NULL
);
go 
CREATE TABLE BlogPosts (
    PostId INT PRIMARY KEY IDENTITY,
    Title NVARCHAR(255) NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    CategoryId INT NOT NULL,
    CONSTRAINT FK_CategoryId FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId) ON DELETE CASCADE
);
go
CREATE TABLE Comments (
    CommentId INT PRIMARY KEY IDENTITY,
    PostId INT NOT NULL,
    Comment NVARCHAR(MAX) NOT NULL,
    CONSTRAINT FK_PostId FOREIGN KEY (PostId) REFERENCES Posts(PostId) ON DELETE CASCADE
);

Categories
Get all categories (GET /api/GetCategorieMaster)
Get a specific category by ID (GET /api/GetCategorieMasterById/id)
Create a new category (POST /api/AddCategories)
Update an existing category (PUT /api/UpdateCategorieMaster/id)


Posts:
Get all posts (GET /api/GetBlogPostsMaster)
Get posts in a specific POST  (GET /api/GetBlogPostsMasterById/ID)
Get a specific category by ID (GET /api/GetBlogPostsMasterByCategory/id)
Create a new post (POST /api/AddBlogPostss)
Update an existing post (PUT /api/UpdateBlogPostsMaster/id)


Comments:
Get all comments for a specific post (GET /api/GetCommentsMasterByPost/id)
Create a new comment (POST /api/AddCommentss)
