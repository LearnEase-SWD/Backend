USE [learnEase]

/* 1. Users */
INSERT INTO Users (UserId, UserName, Email, IsActive, CreatedAt, UpdatedAt)
VALUES ('user-1', 'johndoe', 'johndoe@example.com', 1, '2023-12-31', NULL);

INSERT INTO Users (UserId, UserName, Email, IsActive, CreatedAt, UpdatedAt)
VALUES ('user-2', 'janesmith', 'janesmith@example.com', 1, '2023-12-31', NULL);

INSERT INTO Users (UserId, UserName, Email, IsActive, CreatedAt, UpdatedAt)
VALUES ('user-3', 'alicejohnson', 'alicejohnson@example.com', 1, '2023-12-31', NULL);

INSERT INTO Users (UserId, UserName, Email, IsActive, CreatedAt, UpdatedAt)
VALUES ('user-4', 'bobbrown', 'bobbrown@example.com', 1, '2023-12-31', NULL);

INSERT INTO Users (UserId, UserName, Email, IsActive, CreatedAt, UpdatedAt)
VALUES ('user-5', 'charliedavis', 'charliedavis@example.com', 1, '2023-12-31', NULL);

INSERT INTO Users (UserId, UserName, Email, IsActive, CreatedAt, UpdatedAt)
VALUES ('user-6', 'davidwhite', 'davidwhite@example.com', 1, '2023-12-31', NULL);

INSERT INTO Users (UserId, UserName, Email, IsActive, CreatedAt, UpdatedAt)
VALUES ('user-7', 'emmawilson', 'emmawilson@example.com', 1, '2023-12-31', NULL);

INSERT INTO Users (UserId, UserName, Email, IsActive, CreatedAt, UpdatedAt)
VALUES ('user-8', 'frankmoore', 'frankmoore@example.com', 1, '2023-12-31', NULL);

INSERT INTO Users (UserId, UserName, Email, IsActive, CreatedAt, UpdatedAt)
VALUES ('user-9', 'gracetaylor', 'gracetaylor@example.com', 1, '2023-12-31', NULL);

INSERT INTO Users (UserId, UserName, Email, IsActive, CreatedAt, UpdatedAt)
VALUES ('user-10', 'henryanderson', 'henryanderson@example.com', 1, '2023-12-31', NULL);

/* 2. Roles */
INSERT INTO Roles (RoleId, RoleName) VALUES ('role-1', N'Admin');
INSERT INTO Roles (RoleId, RoleName) VALUES ('role-2', N'Language Expert');
INSERT INTO Roles (RoleId, RoleName) VALUES ('role-3', N'Basic Learner');
INSERT INTO Roles (RoleId, RoleName) VALUES ('role-4', N'VIP Learner');

/* 3. UserDetails */
INSERT INTO UserDetails (Id, FirstName, LastName, Phone, ImageUrl, DateOfBirth, Address, CreatedAt, UpdatedAt, UserId) 
VALUES ('udetail-1', 'John', 'Doe', '123456789', NULL, '1990-01-15', '123 Main St', '2024-01-01 08:30:00', NULL, 'user-1');

INSERT INTO UserDetails (Id, FirstName, LastName, Phone, ImageUrl, DateOfBirth, Address, CreatedAt, UpdatedAt, UserId) 
VALUES ('udetail-2', 'Jane', 'Smith', '987654321', NULL, '1992-06-20', '456 Elm St', '2024-01-02 09:15:00', NULL, 'user-2');

INSERT INTO UserDetails (Id, FirstName, LastName, Phone, ImageUrl, DateOfBirth, Address, CreatedAt, UpdatedAt, UserId) 
VALUES ('udetail-3', 'Alice', 'Johnson', '555123456', NULL, '1995-09-10', '789 Oak St', '2024-01-03 10:45:00', NULL, 'user-3');

INSERT INTO UserDetails (Id, FirstName, LastName, Phone, ImageUrl, DateOfBirth, Address, CreatedAt, UpdatedAt, UserId) 
VALUES ('udetail-4', 'Bob', 'Brown', '666789123', NULL, '1988-12-05', '101 Pine St', '2024-01-04 11:20:00', NULL, 'user-4');

INSERT INTO UserDetails (Id, FirstName, LastName, Phone, ImageUrl, DateOfBirth, Address, CreatedAt, UpdatedAt, UserId) 
VALUES ('udetail-5', 'Charlie', 'Davis', '777456789', NULL, '1991-03-25', '202 Maple St', '2024-01-05 13:10:00', NULL, 'user-5');

INSERT INTO UserDetails (Id, FirstName, LastName, Phone, ImageUrl, DateOfBirth, Address, CreatedAt, UpdatedAt, UserId) 
VALUES ('udetail-6', 'David', 'White', '888987654', NULL, '1993-08-30', '303 Cedar St', '2024-01-06 14:25:00', NULL, 'user-6');

INSERT INTO UserDetails (Id, FirstName, LastName, Phone, ImageUrl, DateOfBirth, Address, CreatedAt, UpdatedAt, UserId) 
VALUES ('udetail-7', 'Emma', 'Wilson', '999123987', NULL, '1997-07-07', '404 Birch St', '2024-01-07 15:00:00', NULL, 'user-7');

INSERT INTO UserDetails (Id, FirstName, LastName, Phone, ImageUrl, DateOfBirth, Address, CreatedAt, UpdatedAt, UserId) 
VALUES ('udetail-8', 'Frank', 'Moore', '111222333', NULL, '1985-11-22', '505 Willow St', '2024-01-08 16:45:00', NULL, 'user-8');

INSERT INTO UserDetails (Id, FirstName, LastName, Phone, ImageUrl, DateOfBirth, Address, CreatedAt, UpdatedAt, UserId) 
VALUES ('udetail-9', 'Grace', 'Taylor', '222333444', NULL, '1996-04-14', '606 Spruce St', '2024-01-09 17:30:00', NULL, 'user-9');

INSERT INTO UserDetails (Id, FirstName, LastName, Phone, ImageUrl, DateOfBirth, Address, CreatedAt, UpdatedAt, UserId) 
VALUES ('udetail-10', 'Henry', 'Anderson', '333444555', NULL, '1994-02-17', '707 Redwood St', '2024-01-10 18:20:00', NULL, 'user-10');

/* 4. Flashcards */
INSERT INTO Flashcards (FlashcardID, Front, Back, PronunciationAudioURL, Category, CreatedAt) 
VALUES ('00000000-0000-0000-0000-000000000001', N'Hello', N'Xin chào', NULL, N'Chào hỏi', '2024-01-01 08:30:00');

INSERT INTO Flashcards (FlashcardID, Front, Back, PronunciationAudioURL, Category, CreatedAt) 
VALUES ('00000000-0000-0000-0000-000000000002', N'Goodbye', N'Tạm biệt', NULL, N'Chào hỏi', '2024-01-02 09:15:00');

INSERT INTO Flashcards (FlashcardID, Front, Back, PronunciationAudioURL, Category, CreatedAt) 
VALUES ('00000000-0000-0000-0000-000000000003', N'Thank you', N'Cảm ơn', NULL, N'Lịch sự', '2024-01-03 10:45:00');

INSERT INTO Flashcards (FlashcardID, Front, Back, PronunciationAudioURL, Category, CreatedAt) 
VALUES ('00000000-0000-0000-0000-000000000004', N'Sorry', N'Xin lỗi', NULL, N'Lịch sự', '2024-01-04 11:20:00');

INSERT INTO Flashcards (FlashcardID, Front, Back, PronunciationAudioURL, Category, CreatedAt) 
VALUES ('00000000-0000-0000-0000-000000000005', N'Apple', N'Táo', NULL, N'Thực phẩm', '2024-01-05 13:10:00');

INSERT INTO Flashcards (FlashcardID, Front, Back, PronunciationAudioURL, Category, CreatedAt) 
VALUES ('00000000-0000-0000-0000-000000000006', N'Car', N'Xe hơi', NULL, N'Phương tiện', '2024-01-06 14:25:00');

INSERT INTO Flashcards (FlashcardID, Front, Back, PronunciationAudioURL, Category, CreatedAt) 
VALUES ('00000000-0000-0000-0000-000000000007', N'Dog', N'Chó', NULL, N'Động vật', '2024-01-07 15:00:00');

INSERT INTO Flashcards (FlashcardID, Front, Back, PronunciationAudioURL, Category, CreatedAt) 
VALUES ('00000000-0000-0000-0000-000000000008', N'Computer', N'Máy tính', NULL, N'Công nghệ', '2024-01-08 16:45:00');

INSERT INTO Flashcards (FlashcardID, Front, Back, PronunciationAudioURL, Category, CreatedAt) 
VALUES ('00000000-0000-0000-0000-000000000009', N'Book', N'Sách', NULL, N'Giáo dục', '2024-01-09 17:30:00');

INSERT INTO Flashcards (FlashcardID, Front, Back, PronunciationAudioURL, Category, CreatedAt) 
VALUES ('00000000-0000-0000-0000-000000000010', N'Music', N'Âm nhạc', NULL, N'Giải trí', '2024-01-10 18:20:00');

/* 5. Leaderboards */
INSERT INTO Leaderboards (RankID, UserID, TotalScore, UpdatedAt) 
VALUES ('10000000-0000-0000-0000-000000000001', 'user-1', 950, '2024-01-01 08:30:00');

INSERT INTO Leaderboards (RankID, UserID, TotalScore, UpdatedAt) 
VALUES ('10000000-0000-0000-0000-000000000002', 'user-2', 870, '2024-01-02 09:15:00');

INSERT INTO Leaderboards (RankID, UserID, TotalScore, UpdatedAt) 
VALUES ('10000000-0000-0000-0000-000000000003', 'user-3', 820, '2024-01-03 10:45:00');

INSERT INTO Leaderboards (RankID, UserID, TotalScore, UpdatedAt) 
VALUES ('10000000-0000-0000-0000-000000000004', 'user-4', 780, '2024-01-04 11:20:00');

INSERT INTO Leaderboards (RankID, UserID, TotalScore, UpdatedAt) 
VALUES ('10000000-0000-0000-0000-000000000005', 'user-5', 740, '2024-01-05 13:10:00');

INSERT INTO Leaderboards (RankID, UserID, TotalScore, UpdatedAt) 
VALUES ('10000000-0000-0000-0000-000000000006', 'user-6', 690, '2024-01-06 14:25:00');

INSERT INTO Leaderboards (RankID, UserID, TotalScore, UpdatedAt) 
VALUES ('10000000-0000-0000-0000-000000000007', 'user-7', 660, '2024-01-07 15:00:00');

INSERT INTO Leaderboards (RankID, UserID, TotalScore, UpdatedAt) 
VALUES ('10000000-0000-0000-0000-000000000008', 'user-8', 620, '2024-01-08 16:45:00');

INSERT INTO Leaderboards (RankID, UserID, TotalScore, UpdatedAt) 
VALUES ('10000000-0000-0000-0000-000000000009', 'user-9', 580, '2024-01-09 17:30:00');

INSERT INTO Leaderboards (RankID, UserID, TotalScore, UpdatedAt) 
VALUES ('10000000-0000-0000-0000-000000000010', 'user-10', 550, '2024-01-10 18:20:00');

/* 6. Subscriptions */
INSERT INTO Subscriptions (SubscriptionID, UserID, PlanType, StartDate, EndDate, Status) 
VALUES ('20000000-0000-0000-0000-000000000001', 'user-1', 'Monthly', '2024-01-01', '2024-01-31', 'Expired');

INSERT INTO Subscriptions (SubscriptionID, UserID, PlanType, StartDate, EndDate, Status) 
VALUES ('20000000-0000-0000-0000-000000000002', 'user-2', 'Yearly', '2024-01-02', '2025-01-01', 'Active');

INSERT INTO Subscriptions (SubscriptionID, UserID, PlanType, StartDate, EndDate, Status) 
VALUES ('20000000-0000-0000-0000-000000000003', 'user-3', 'Monthly', '2024-01-03', '2024-02-02', 'Active');

INSERT INTO Subscriptions (SubscriptionID, UserID, PlanType, StartDate, EndDate, Status) 
VALUES ('20000000-0000-0000-0000-000000000004', 'user-4', 'Yearly', '2024-01-04', '2025-01-03', 'Active');

INSERT INTO Subscriptions (SubscriptionID, UserID, PlanType, StartDate, EndDate, Status) 
VALUES ('20000000-0000-0000-0000-000000000005', 'user-5', 'Monthly', '2024-01-05', '2024-02-04', 'Expired');

INSERT INTO Subscriptions (SubscriptionID, UserID, PlanType, StartDate, EndDate, Status) 
VALUES ('20000000-0000-0000-0000-000000000006', 'user-6', 'Yearly', '2024-01-06', '2025-01-05', 'Active');

INSERT INTO Subscriptions (SubscriptionID, UserID, PlanType, StartDate, EndDate, Status) 
VALUES ('20000000-0000-0000-0000-000000000007', 'user-7', 'Monthly', '2024-01-07', '2024-02-06', 'Active');

INSERT INTO Subscriptions (SubscriptionID, UserID, PlanType, StartDate, EndDate, Status) 
VALUES ('20000000-0000-0000-0000-000000000008', 'user-8', 'Yearly', '2024-01-08', '2025-01-07', 'Active');

INSERT INTO Subscriptions (SubscriptionID, UserID, PlanType, StartDate, EndDate, Status) 
VALUES ('20000000-0000-0000-0000-000000000009', 'user-9', 'Monthly', '2024-01-09', '2024-02-08', 'Active');

INSERT INTO Subscriptions (SubscriptionID, UserID, PlanType, StartDate, EndDate, Status) 
VALUES ('20000000-0000-0000-0000-000000000010', 'user-10', 'Yearly', '2024-01-10', '2025-01-09', 'Active');

/* 7. Courses */
INSERT INTO Courses (CourseID, CourseName, CourseDescription, Language, DifficultyLevel, CreatedAt, UpdatedAt) 
VALUES ('a1b2c3d4-e5f6-7890-abcd-1234567890ab', N'Giao tiếp tiếng Anh cơ bản', N'Khóa học giúp bạn làm quen với các tình huống giao tiếp hàng ngày.', N'English', N'Beginner', '2024-01-01', NULL);

INSERT INTO Courses (CourseID, CourseName, CourseDescription, Language, DifficultyLevel, CreatedAt, UpdatedAt) 
VALUES ('b2c3d4e5-f678-9012-abcd-2345678901bc', N'Tiếng Anh trung cấp', N'Khóa học giúp cải thiện khả năng giao tiếp và viết tiếng Anh.', N'English', N'Intermediate', '2024-02-11', NULL);

INSERT INTO Courses (CourseID, CourseName, CourseDescription, Language, DifficultyLevel, CreatedAt, UpdatedAt) 
VALUES ('c3d4e5f6-7890-1234-abcd-3456789012cd', N'Tiếng Anh nâng cao', N'Dành cho những người muốn thành thạo tiếng Anh.', N'English', N'Advanced', '2024-02-11', NULL);

INSERT INTO Courses (CourseID, CourseName, CourseDescription, Language, DifficultyLevel, CreatedAt, UpdatedAt) 
VALUES ('d4e5f678-9012-3456-abcd-4567890123de', N'Tiếng Nhật sơ cấp', N'Bước đầu làm quen với tiếng Nhật và bảng chữ cái.', N'Japanese', N'Beginner', '2024-02-11', NULL);

INSERT INTO Courses (CourseID, CourseName, CourseDescription, Language, DifficultyLevel, CreatedAt, UpdatedAt) 
VALUES ('e5f67890-1234-5678-abcd-5678901234ef', N'Tiếng Nhật trung cấp', N'Khóa học giúp bạn nâng cao kỹ năng đọc và giao tiếp tiếng Nhật.', N'Japanese', N'Intermediate', '2024-02-11', NULL);

/* 8. UserCourses */
INSERT INTO UserCourses (UserCourseID, UserID, CourseID, EnrollmentDate, ProgressStatus, ProgressPercentage, CompletionDate) 
VALUES ('a1f2c3d4-e5f6-4789-bcde-1234567890a1', 'user-1', 'a1b2c3d4-e5f6-7890-abcd-1234567890ab', '2024-02-01', N'In Progress', 50, NULL);

INSERT INTO UserCourses (UserCourseID, UserID, CourseID, EnrollmentDate, ProgressStatus, ProgressPercentage, CompletionDate) 
VALUES ('b2f3d4e5-f678-4901-bcde-2345678901b2', 'user-2', 'b2c3d4e5-f678-9012-abcd-2345678901bc', '2024-02-03', N'Completed', 100, '2024-02-10');

INSERT INTO UserCourses (UserCourseID, UserID, CourseID, EnrollmentDate, ProgressStatus, ProgressPercentage, CompletionDate) 
VALUES ('c3f4e5f6-7890-4123-bcde-3456789012c3', 'user-3', 'c3d4e5f6-7890-1234-abcd-3456789012cd', '2024-02-05', N'In Progress', 30, NULL);

INSERT INTO UserCourses (UserCourseID, UserID, CourseID, EnrollmentDate, ProgressStatus, ProgressPercentage, CompletionDate) 
VALUES ('d4f5e678-9012-4345-bcde-4567890123d4', 'user-4', 'd4e5f678-9012-3456-abcd-4567890123de', '2024-02-07', N'Not Started', 0, NULL);

INSERT INTO UserCourses (UserCourseID, UserID, CourseID, EnrollmentDate, ProgressStatus, ProgressPercentage, CompletionDate) 
VALUES ('e5f67890-1234-4567-bcde-5678901234e5', 'user-5', 'e5f67890-1234-5678-abcd-5678901234ef', '2024-02-08', N'In Progress', 20, NULL);

INSERT INTO UserCourses (UserCourseID, UserID, CourseID, EnrollmentDate, ProgressStatus, ProgressPercentage, CompletionDate) 
VALUES ('f6f78901-2345-4789-bcde-6789012345f6', 'user-6', 'a1b2c3d4-e5f6-7890-abcd-1234567890ab', '2024-02-09', N'Completed', 100, '2024-02-15');

/* 9. Lessons */
INSERT INTO Lessons (LessonID, CourseID, Title, LessonType, CreatedAt) 
VALUES ('1a2b3c4d-d5e6-4789-bcde-1234567890ab', 'a1b2c3d4-e5f6-7890-abcd-1234567890ab', N'Chào hỏi cơ bản', N'Video', '2024-02-01');

INSERT INTO Lessons (LessonID, CourseID, Title, LessonType, CreatedAt) 
VALUES ('2b3c4d5e-f6e7-4901-bcde-2345678901ac', 'a1b2c3d4-e5f6-7890-abcd-1234567890ab', N'Mẫu câu giao tiếp hàng ngày', N'Lý thuyết', '2024-02-02');

INSERT INTO Lessons (LessonID, CourseID, Title, LessonType, CreatedAt) 
VALUES ('3c4d5e6f-f789-4123-bcde-3456789012ad', 'b2c3d4e5-f678-9012-abcd-2345678901bc', N'Ngữ pháp cơ bản', N'Lý thuyết', '2024-02-03');

INSERT INTO Lessons (LessonID, CourseID, Title, LessonType, CreatedAt) 
VALUES ('4d5e6f70-8901-4345-bcde-4567890123ae', 'b2c3d4e5-f678-9012-abcd-2345678901bc', N'Luyện nói: Mô tả bản thân', N'Hội thoại', '2024-02-04');

INSERT INTO Lessons (LessonID, CourseID, Title, LessonType, CreatedAt) 
VALUES ('5e6f7890-0123-4567-bcde-5678901234af', 'c3d4e5f6-7890-1234-abcd-3456789012cd', N'Thảo luận nâng cao', N'Hội thoại', '2024-02-05');

INSERT INTO Lessons (LessonID, CourseID, Title, LessonType, CreatedAt) 
VALUES ('6f789012-2345-4789-bcde-6789012345b0', 'c3d4e5f6-7890-1234-abcd-3456789012cd', N'Viết luận chuyên sâu', N'Bài tập', '2024-02-06');

INSERT INTO Lessons (LessonID, CourseID, Title, LessonType, CreatedAt) 
VALUES ('7a890123-3456-4901-bcde-7890123456b1', 'd4e5f678-9012-3456-abcd-4567890123de', N'Giới thiệu bảng chữ cái tiếng Nhật', N'Video', '2024-02-07');

INSERT INTO Lessons (LessonID, CourseID, Title, LessonType, CreatedAt) 
VALUES ('8b901234-4567-4123-bcde-8901234567b2', 'd4e5f678-9012-3456-abcd-4567890123de', N'Phát âm Hiragana và Katakana', N'Lý thuyết', '2024-02-08');

INSERT INTO Lessons (LessonID, CourseID, Title, LessonType, CreatedAt) 
VALUES ('9c012345-5678-4345-bcde-9012345678b3', 'e5f67890-1234-5678-abcd-5678901234ef', N'Đọc hiểu đoạn văn', N'Bài tập', '2024-02-09');

INSERT INTO Lessons (LessonID, CourseID, Title, LessonType, CreatedAt) 
VALUES ('0d123456-6789-4567-bcde-0123456789b4', 'e5f67890-1234-5678-abcd-5678901234ef', N'Luyện nghe hội thoại', N'Hội thoại', '2024-02-10');

/* 10. TheoryLesson */
INSERT INTO TheoryLessons (TheoryID, LessonID, Content, Examples, CreatedAt) 
VALUES ('7a2b3c4d-d5e6-4789-bcde-1234567890ab', '1a2b3c4d-d5e6-4789-bcde-1234567890ab', N'Giới thiệu về ngữ pháp cơ bản', N'Ví dụ: Tôi là học sinh.', '2024-02-01');

INSERT INTO TheoryLessons (TheoryID, LessonID, Content, Examples, CreatedAt) 
VALUES ('8b3c4d5e-f6e7-4901-bcde-2345678901ac', '2b3c4d5e-f6e7-4901-bcde-2345678901ac', N'Giới thiệu các loại động từ trong tiếng Anh', N'Ví dụ: I play, She runs, They are studying.', '2024-02-02');

INSERT INTO TheoryLessons (TheoryID, LessonID, Content, Examples, CreatedAt) 
VALUES ('9c4d5e6f-f789-4123-bcde-3456789012ad', '3c4d5e6f-f789-4123-bcde-3456789012ad', N'Giới thiệu về các thì trong tiếng Anh', N'Ví dụ: I am learning English. Ví dụ 2: He will go to the market.', '2024-02-03');

INSERT INTO TheoryLessons (TheoryID, LessonID, Content, Examples, CreatedAt) 
VALUES ('10d5e6f7-8901-4345-bcde-4567890123ae', '4d5e6f70-8901-4345-bcde-4567890123ae', N'Giới thiệu về các loại câu trong tiếng Anh', N'Ví dụ: Do you like coffee? She is reading a book.', '2024-02-04');

INSERT INTO TheoryLessons (TheoryID, LessonID, Content, Examples, CreatedAt) 
VALUES ('13e6f789-0123-4567-bcde-5678901234af', '5e6f7890-0123-4567-bcde-5678901234af', N'Giới thiệu về cách sử dụng giới từ', N'Ví dụ: I am in the room. He is on the table.', '2024-02-05');

/* 11. VideoLessons */
INSERT INTO VideoLessons (VideoID, LessonID, VideoURL, Transcript, Duration, CreatedAt) 
VALUES ('f1a2b3c4-d5e6-4789-bcde-1234567890fa', '1a2b3c4d-d5e6-4789-bcde-1234567890ab', 'https://example.com/video1', N'Video về Chào hỏi cơ bản', '00:10:00', '2024-02-01');

INSERT INTO VideoLessons (VideoID, LessonID, VideoURL, Transcript, Duration, CreatedAt) 
VALUES ('f2b3c4d5-f6e7-4901-bcde-2345678901fb', '2b3c4d5e-f6e7-4901-bcde-2345678901ac', 'https://example.com/video2', N'Video về Mẫu câu giao tiếp hàng ngày', '00:15:00', '2024-02-02');

INSERT INTO VideoLessons (VideoID, LessonID, VideoURL, Transcript, Duration, CreatedAt) 
VALUES ('f3c4d5e6-f789-4123-bcde-3456789012fc', '3c4d5e6f-f789-4123-bcde-3456789012ad', 'https://example.com/video3', N'Video về Ngữ pháp cơ bản', '00:20:00', '2024-02-03');

INSERT INTO VideoLessons (VideoID, LessonID, VideoURL, Transcript, Duration, CreatedAt) 
VALUES ('f4d5e6f7-8901-4345-bcde-4567890123fd', '4d5e6f70-8901-4345-bcde-4567890123ae', 'https://example.com/video4', N'Video về Luyện nói: Mô tả bản thân', '00:18:00', '2024-02-04');

INSERT INTO VideoLessons (VideoID, LessonID, VideoURL, Transcript, Duration, CreatedAt) 
VALUES ('f5e6f789-0123-4567-bcde-5678901234fe', '5e6f7890-0123-4567-bcde-5678901234af', 'https://example.com/video5', N'Video về Thảo luận nâng cao', '00:25:00', '2024-02-05');

/* 12. Exercises */
INSERT INTO Exercises (ExerciseID, LessonID, Type, Question, CorrectAnswer, CreatedAt) 
VALUES ('f3a7b8c5-10a1-4c35-91b3-d2ab87a48f39', '1a2b3c4d-d5e6-4789-bcde-1234567890ab', N'Trắc nghiệm', N'Câu hỏi 1: Chào hỏi như thế nào?', N'Xin chào', '2024-02-01');

INSERT INTO Exercises (ExerciseID, LessonID, Type, Question, CorrectAnswer, CreatedAt) 
VALUES ('c8d6e9b2-7865-4d6b-a4b2-56ac3f8d7e1e', '2b3c4d5e-f6e7-4901-bcde-2345678901ac', N'Điền từ', N'Chúng tôi ___ học tiếng Anh.', N'đang', '2024-02-02');

INSERT INTO Exercises (ExerciseID, LessonID, Type, Question, CorrectAnswer, CreatedAt) 
VALUES ('b5a0d8c3-7e9a-4fe4-9d4c-1f0a073f0a72', '3c4d5e6f-f789-4123-bcde-3456789012ad', N'Sắp xếp câu', N'Sắp xếp: học / chúng / tiếng / tôi / Anh', N'Tôi học tiếng Anh', '2024-02-03');

INSERT INTO Exercises (ExerciseID, LessonID, Type, Question, CorrectAnswer, CreatedAt) 
VALUES ('a8e6b4f7-9f10-47e5-b736-11cd82626323', '4d5e6f70-8901-4345-bcde-4567890123ae', N'Trắc nghiệm', N'Câu hỏi 2: Cách giới thiệu bản thân?', N'Tôi tên là ...', '2024-02-04');

INSERT INTO Exercises (ExerciseID, LessonID, Type, Question, CorrectAnswer, CreatedAt) 
VALUES ('d4a99c5d-850d-41a2-9868-0bb567e4a745', '5e6f7890-0123-4567-bcde-5678901234af', N'Điền từ', N'Học tiếng ___ là quan trọng.', N'Anh', '2024-02-05');

/* 13. UserExercises */
INSERT INTO UserExercises (UserExerciseID, UserID, ExerciseID, CompletionStatus, Score, SubmissionDate, CreatedAt) 
VALUES ('f1a2d3e4-8b5c-47d8-9a7f-0b6c1a3b4f56', 'user-1', 'f3a7b8c5-10a1-4c35-91b3-d2ab87a48f39', N'Completed', 80, '2024-02-01', '2024-02-01');

INSERT INTO UserExercises (UserExerciseID, UserID, ExerciseID, CompletionStatus, Score, SubmissionDate, CreatedAt) 
VALUES ('c2b9d8a3-2f7e-48b1-98b2-04d97b1c9d8d', 'user-2', 'c8d6e9b2-7865-4d6b-a4b2-56ac3f8d7e1e', N'In Progress', 50, '2024-02-02', '2024-02-02');

INSERT INTO UserExercises (UserExerciseID, UserID, ExerciseID, CompletionStatus, Score, SubmissionDate, CreatedAt) 
VALUES ('b3e6f7a1-6d1c-4e7e-94a9-d4b1c6a8a7a0', 'user-3', 'b5a0d8c3-7e9a-4fe4-9d4c-1f0a073f0a72', N'Failed', 40, '2024-02-03', '2024-02-03');

INSERT INTO UserExercises (UserExerciseID, UserID, ExerciseID, CompletionStatus, Score, SubmissionDate, CreatedAt) 
VALUES ('d4b8f3a0-9e2b-462f-a02b-1a8c8a3d9e62', 'user-4', 'a8e6b4f7-9f10-47e5-b736-11cd82626323', N'Completed', 90, '2024-02-04', '2024-02-04');

INSERT INTO UserExercises (UserExerciseID, UserID, ExerciseID, CompletionStatus, Score, SubmissionDate, CreatedAt) 
VALUES ('e5f8b6a2-1b8f-4e0b-bf90-3c9a6f9d3b75', 'user-5', 'd4a99c5d-850d-41a2-9868-0bb567e4a745', N'In Progress', 70, '2024-02-05', '2024-02-05');

/* 14. Forums */
INSERT INTO Forums (ForumID, CourseID, Description, CreatedAt) 
VALUES ('f1a2b3c4-d5e6-7890-abcd-6789012345a1', 'a1b2c3d4-e5f6-7890-abcd-1234567890ab', N'Forum dành cho khóa học Giao tiếp tiếng Anh cơ bản', '2024-02-11');

INSERT INTO Forums (ForumID, CourseID, Description, CreatedAt) 
VALUES ('f2b3c4d5-e6f7-8901-bcde-7890123456b2', 'b2c3d4e5-f678-9012-abcd-2345678901bc', N'Forum dành cho khóa học Tiếng Anh trung cấp', '2024-02-11');

INSERT INTO Forums (ForumID, CourseID, Description, CreatedAt) 
VALUES ('f3c4d5e6-f789-0123-cdef-8901234567c3', 'c3d4e5f6-7890-1234-abcd-3456789012cd', N'Forum dành cho khóa học Tiếng Anh nâng cao', '2024-02-11');

INSERT INTO Forums (ForumID, CourseID, Description, CreatedAt) 
VALUES ('f4d5e6f7-8901-2345-def0-9012345678d4', 'd4e5f678-9012-3456-abcd-4567890123de', N'Forum dành cho khóa học Tiếng Nhật sơ cấp', '2024-02-11');

INSERT INTO Forums (ForumID, CourseID, Description, CreatedAt) 
VALUES ('f5e6f789-0123-4567-ef01-0123456789e5', 'e5f67890-1234-5678-abcd-5678901234ef', N'Forum dành cho khóa học Tiếng Nhật trung cấp', '2024-02-11');

/* 15. Topics */
INSERT INTO Topics (TopicID, ForumID, Title, Description, CreatedAt) 
VALUES ('1a2b3c4d-5e6f-7890-abcd-1234567890a1', 'f1a2b3c4-d5e6-7890-abcd-6789012345a1', 
        N'Hỏi đáp bài tập', 
        N'Thảo luận về các bài tập trong khóa học Giao tiếp tiếng Anh cơ bản', 
        '2024-02-11');

INSERT INTO Topics (TopicID, ForumID, Title, Description, CreatedAt) 
VALUES ('2b3c4d5e-6f78-9012-bcde-2345678901b2', 'f2b3c4d5-e6f7-8901-bcde-7890123456b2', 
        N'Thảo luận', 
        N'Chia sẻ kinh nghiệm và phương pháp học Tiếng Anh trung cấp', 
        '2024-02-11');

INSERT INTO Topics (TopicID, ForumID, Title, Description, CreatedAt) 
VALUES ('3c4d5e6f-7890-1234-cdef-3456789012c3', 'f3c4d5e6-f789-0123-cdef-8901234567c3', 
        N'Góp ý khóa học', 
        N'Đóng góp ý kiến để cải thiện khóa học Tiếng Anh nâng cao', 
        '2024-02-11');

INSERT INTO Topics (TopicID, ForumID, Title, Description, CreatedAt) 
VALUES ('4d5e6f78-9012-3456-def0-4567890123d4', 'f4d5e6f7-8901-2345-def0-9012345678d4', 
        N'Học cùng nhau', 
        N'Kết nối và học tập cùng các bạn học viên Tiếng Nhật sơ cấp', 
        '2024-02-11');

INSERT INTO Topics (TopicID, ForumID, Title, Description, CreatedAt) 
VALUES ('5e6f7890-1234-5678-ef01-5678901234e5', 'f5e6f789-0123-4567-ef01-0123456789e5', 
        N'Tài liệu tham khảo', 
        N'Chia sẻ tài liệu và mẹo học Tiếng Nhật trung cấp', 
        '2024-02-11');

/* 16. Posts */
INSERT INTO Posts (PostID, TopicID, UserID, Title, ContentHtml, LikeCount, DislikeCount, CreatedAt) 
VALUES ('6a7b8c9d-0e1f-2345-abcd-6789012345a1', '1a2b3c4d-5e6f-7890-abcd-1234567890a1', 'user-1', 
        N'Cách giải bài tập số 5', 
        N'<p>Mình gặp khó khăn khi làm bài tập số 5, mọi người có thể giúp mình không?</p>', 
        10, 0, '2024-02-11');

INSERT INTO Posts (PostID, TopicID, UserID, Title, ContentHtml, LikeCount, DislikeCount, CreatedAt) 
VALUES ('7b8c9d0e-1f23-4567-bcde-7890123456b2', '2b3c4d5e-6f78-9012-bcde-2345678901b2', 'user-2', 
        N'Phương pháp luyện nghe hiệu quả', 
        N'<p>Mọi người có thể chia sẻ phương pháp luyện nghe tiếng Anh trung cấp không?</p>', 
        25, 2, '2024-02-11');

INSERT INTO Posts (PostID, TopicID, UserID, Title, ContentHtml, LikeCount, DislikeCount, CreatedAt) 
VALUES ('8c9d0e1f-2345-6789-cdef-8901234567c3', '3c4d5e6f-7890-1234-cdef-3456789012c3', 'user-3', 
        N'Góp ý về phần phát âm', 
        N'<p>Mình thấy phần phát âm còn hơi khó, có thể bổ sung thêm bài tập thực hành không?</p>', 
        18, 1, '2024-02-11');

INSERT INTO Posts (PostID, TopicID, UserID, Title, ContentHtml, LikeCount, DislikeCount, CreatedAt) 
VALUES ('9d0e1f23-4567-8901-def0-9012345678d4', '4d5e6f78-9012-3456-def0-4567890123d4', 'user-4', 
        N'Mẹo học bảng chữ cái tiếng Nhật', 
        N'<p>Mọi người có thể chia sẻ mẹo học nhanh bảng chữ cái hiragana và katakana không?</p>', 
        30, 0, '2024-02-11');

INSERT INTO Posts (PostID, TopicID, UserID, Title, ContentHtml, LikeCount, DislikeCount, CreatedAt) 
VALUES ('0e1f2345-6789-0123-ef01-0123456789e5', '5e6f7890-1234-5678-ef01-5678901234e5', 'user-5', 
        N'Sách luyện đọc tiếng Nhật hay', 
        N'<p>Các bạn có thể giới thiệu một số sách luyện đọc tiếng Nhật trung cấp được không?</p>', 
        22, 3, '2024-02-11');

/* 17. Comments */
INSERT INTO Comments (CommentID, PostID, UserID, CommentText, CreatedAt) 
VALUES ('a1b2c3d4-e5f6-7890-abcd-1234567890f1', '6a7b8c9d-0e1f-2345-abcd-6789012345a1', 'user-6', 
        N'Mình cũng gặp khó khăn với bài này, bạn đã thử cách nào chưa?', 
        '2024-02-11');

INSERT INTO Comments (CommentID, PostID, UserID, CommentText, CreatedAt) 
VALUES ('b2c3d4e5-f678-9012-bcde-2345678901f2', '7b8c9d0e-1f23-4567-bcde-7890123456b2', 'user-7', 
        N'Mình thấy luyện nghe theo phương pháp shadowing khá hiệu quả.', 
        '2024-02-11');

INSERT INTO Comments (CommentID, PostID, UserID, CommentText, CreatedAt) 
VALUES ('c3d4e5f6-7890-1234-cdef-3456789012f3', '8c9d0e1f-2345-6789-cdef-8901234567c3', 'user-8', 
        N'Bài phát âm đó đúng là khó thật, mình cũng phải luyện rất nhiều.', 
        '2024-02-11');

INSERT INTO Comments (CommentID, PostID, UserID, CommentText, CreatedAt) 
VALUES ('d4e5f678-9012-3456-def0-4567890123f4', '9d0e1f23-4567-8901-def0-9012345678d4', 'user-9', 
        N'Mình sử dụng ứng dụng Anki để nhớ chữ cái nhanh hơn.', 
        '2024-02-11');

INSERT INTO Comments (CommentID, PostID, UserID, CommentText, CreatedAt) 
VALUES ('e5f67890-1234-5678-ef01-5678901234f5', '0e1f2345-6789-0123-ef01-0123456789e5', 'user-10', 
        N'Mình thấy sách "Minna no Nihongo" khá hữu ích để luyện đọc.', 
        '2024-02-11');

/* 18. UserProgress */
INSERT INTO UserProgress (ProgressID, UserID, LessonID, CompletionStatus, Score, LastAccessed)
VALUES ('a1b2c3d4-e5f6-4789-bcde-1234567890a1', 'user-1', '1a2b3c4d-d5e6-4789-bcde-1234567890ab', 'In Progress', 85, '2024-02-11');

INSERT INTO UserProgress (ProgressID, UserID, LessonID, CompletionStatus, Score, LastAccessed)
VALUES ('b2c3d4e5-f678-4901-bcde-2345678901b2', 'user-2', '2b3c4d5e-f6e7-4901-bcde-2345678901ac', 'Completed', 95, '2024-02-10');

INSERT INTO UserProgress (ProgressID, UserID, LessonID, CompletionStatus, Score, LastAccessed)
VALUES ('c3d4e5f6-7890-4123-bcde-3456789012c3', 'user-3', '3c4d5e6f-f789-4123-bcde-3456789012ad', 'In Progress', 70, '2024-02-09');

INSERT INTO UserProgress (ProgressID, UserID, LessonID, CompletionStatus, Score, LastAccessed)
VALUES ('d4e5f678-9012-4345-bcde-4567890123d4', 'user-4', '4d5e6f70-8901-4345-bcde-4567890123ae', 'Completed', 88, '2024-02-08');

INSERT INTO UserProgress (ProgressID, UserID, LessonID, CompletionStatus, Score, LastAccessed)
VALUES ('e5f67890-1234-4567-bcde-5678901234e5', 'user-5', '5e6f7890-0123-4567-bcde-5678901234af', 'In Progress', 60, '2024-02-07');

INSERT INTO UserProgress (ProgressID, UserID, LessonID, CompletionStatus, Score, LastAccessed)
VALUES ('f7890123-2345-4789-bcde-6789012345f6', 'user-6', '6f789012-2345-4789-bcde-6789012345b0', 'Completed', 92, '2024-02-06');

INSERT INTO UserProgress (ProgressID, UserID, LessonID, CompletionStatus, Score, LastAccessed)
VALUES ('a8901234-3456-4901-bcde-7890123456a7', 'user-7', '7a890123-3456-4901-bcde-7890123456b1', 'In Progress', 75, '2024-02-05');

INSERT INTO UserProgress (ProgressID, UserID, LessonID, CompletionStatus, Score, LastAccessed)
VALUES ('b9012345-4567-4123-bcde-8901234567b8', 'user-8', '8b901234-4567-4123-bcde-8901234567b2', 'Completed', 80, '2024-02-04');

INSERT INTO UserProgress (ProgressID, UserID, LessonID, CompletionStatus, Score, LastAccessed)
VALUES ('c0123456-5678-4345-bcde-9012345678c9', 'user-9', '9c012345-5678-4345-bcde-9012345678b3', 'In Progress', 68, '2024-02-03');

INSERT INTO UserProgress (ProgressID, UserID, LessonID, CompletionStatus, Score, LastAccessed)
VALUES ('d1234567-6789-4567-bcde-0123456789d0', 'user-10', '0d123456-6789-4567-bcde-0123456789b4', 'Completed', 90, '2024-02-02');

/* 19. UserFlashcard */
INSERT INTO UserFlashcards (UserFlashcardID, UserID, FlashcardID, ProficiencyLevel, ReviewCount, LastReviewedAt)
VALUES ('1a2b3c4d-5e6f-4789-bcde-1234567890a1', 'user-1', '00000000-0000-0000-0000-000000000001', 'Beginner', 3, '2024-02-11');

INSERT INTO UserFlashcards (UserFlashcardID, UserID, FlashcardID, ProficiencyLevel, ReviewCount, LastReviewedAt)
VALUES ('2b3c4d5e-6f78-4901-bcde-2345678901b2', 'user-2', '00000000-0000-0000-0000-000000000002', 'Intermediate', 5, '2024-02-10');

INSERT INTO UserFlashcards (UserFlashcardID, UserID, FlashcardID, ProficiencyLevel, ReviewCount, LastReviewedAt)
VALUES ('3c4d5e6f-7890-4123-bcde-3456789012c3', 'user-3', '00000000-0000-0000-0000-000000000003', 'Advanced', 8, '2024-02-09');

INSERT INTO UserFlashcards (UserFlashcardID, UserID, FlashcardID, ProficiencyLevel, ReviewCount, LastReviewedAt)
VALUES ('4d5e6f70-8901-4345-bcde-4567890123d4', 'user-4', '00000000-0000-0000-0000-000000000004', 'Beginner', 2, '2024-02-08');

INSERT INTO UserFlashcards (UserFlashcardID, UserID, FlashcardID, ProficiencyLevel, ReviewCount, LastReviewedAt)
VALUES ('5e6f7890-1234-4567-bcde-5678901234e5', 'user-5', '00000000-0000-0000-0000-000000000005', 'Intermediate', 6, '2024-02-07');

INSERT INTO UserFlashcards (UserFlashcardID, UserID, FlashcardID, ProficiencyLevel, ReviewCount, LastReviewedAt)
VALUES ('6f789012-3456-4789-bcde-6789012345f6', 'user-6', '00000000-0000-0000-0000-000000000006', 'Advanced', 10, '2024-02-06');

INSERT INTO UserFlashcards (UserFlashcardID, UserID, FlashcardID, ProficiencyLevel, ReviewCount, LastReviewedAt)
VALUES ('7a890123-4567-4901-bcde-7890123456e7', 'user-7', '00000000-0000-0000-0000-000000000007', 'Beginner', 1, '2024-02-05');

INSERT INTO UserFlashcards (UserFlashcardID, UserID, FlashcardID, ProficiencyLevel, ReviewCount, LastReviewedAt)
VALUES ('8b901234-5678-4123-bcde-8901234567e8', 'user-8', '00000000-0000-0000-0000-000000000008', 'Intermediate', 4, '2024-02-04');

INSERT INTO UserFlashcards (UserFlashcardID, UserID, FlashcardID, ProficiencyLevel, ReviewCount, LastReviewedAt)
VALUES ('9c012345-6789-4345-bcde-9012345678e9', 'user-9', '00000000-0000-0000-0000-000000000009', 'Advanced', 7, '2024-02-03');

INSERT INTO UserFlashcards (UserFlashcardID, UserID, FlashcardID, ProficiencyLevel, ReviewCount, LastReviewedAt)
VALUES ('0d123456-7890-4567-bcde-0123456789e0', 'user-10', '00000000-0000-0000-0000-000000000010', 'Beginner', 3, '2024-02-02');

/* 20. UserRole */
INSERT INTO UserRoles (UserId, RoleId) VALUES ('user-1', 'role-1'); 
INSERT INTO UserRoles (UserId, RoleId) VALUES ('user-2', 'role-2');
INSERT INTO UserRoles (UserId, RoleId) VALUES ('user-3', 'role-3'); 
INSERT INTO UserRoles (UserId, RoleId) VALUES ('user-4', 'role-4'); 
INSERT INTO UserRoles (UserId, RoleId) VALUES ('user-5', 'role-3'); 
INSERT INTO UserRoles (UserId, RoleId) VALUES ('user-6', 'role-4'); 
INSERT INTO UserRoles (UserId, RoleId) VALUES ('user-7', 'role-3'); 
INSERT INTO UserRoles (UserId, RoleId) VALUES ('user-8', 'role-4'); 
INSERT INTO UserRoles (UserId, RoleId) VALUES ('user-9', 'role-2'); 
INSERT INTO UserRoles (UserId, RoleId) VALUES ('user-10', 'role-3'); 
