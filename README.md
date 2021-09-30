# Imagegram

API for Instagram copy, allowing users to create accounts, upload images and comment on them.

Project created with .NET6. 

## How to run?

1) Create Postgre database and put the credentials into `server\Mijalski.Imagegram.Server\appsettings.Development.json`
2) Run the project with `dotnet build && dotnet run`

![Swagger Documentation](https://i.imgur.com/nHEcHae.png)

## Functionality

1) Ability to create new accounts
2) Ability to login
3) Ability to remove account with all data (posts, comments)
4) Ability to add posts with images (only accepting JPG)
5) Ability to add comments to posts
6) Ability to get list of posts ordered by comment count (showing only 2 most recent) [skip/take based pagination]

## How to test API?

POST name and password to /accounts to create account
![Step1](https://i.imgur.com/VIs8AVh.png)

POST name and password to /accounts/login to receive the token
![Step2](https://i.imgur.com/z1TKSSD.png)

To create post you need to be authorized (append the token in header, like so: `Authentication: Bearer {MY-TOKEN}`
![Step3](https://i.imgur.com/Z4SOouZ.png)

POST to /posts sample image PNG data with caption, in return you will receive the POST ID
![Step4](https://i.imgur.com/1gEWB2E.png)

POST to /posts/{POST-ID}/comments with some comment to create one
![Step5](https://i.imgur.com/onFDhA4.png)

GET /posts to receive the list of images, by comment count descending
![Step6](https://i.imgur.com/ruCsIuh.png)

DELETE /accounts to remove all your data
