USE [learnEase]

/* 1. Users */
INSERT INTO Users (UserId, UserName, Email, FirstName, LastName, ImageUrl, Role, IsActive, CreatedAt)
VALUES ('user-1', 'nguyenvana', 'nguyenvana@example.com', N'Nguyễn Văn', N'A', NULL, 1, 1, '2023-12-31');

INSERT INTO Users (UserId, UserName, Email, FirstName, LastName, ImageUrl, Role, IsActive, CreatedAt)
VALUES ('user-2', 'tranthib', 'tranthib@example.com', N'Trần Thị', N'B', NULL, 1, 1, '2023-12-31');

INSERT INTO Users (UserId, UserName, Email, FirstName, LastName, ImageUrl, Role, IsActive, CreatedAt)
VALUES ('user-3', 'lebichc', 'lebichc@example.com', N'Lê Bích', N'C', NULL, 1, 1, '2023-12-31');

INSERT INTO Users (UserId, UserName, Email, FirstName, LastName, ImageUrl, Role, IsActive, CreatedAt)
VALUES ('user-4', 'phamvand', 'phamvand@example.com', N'Phạm Văn', N'D', NULL, 1, 1, '2023-12-31');

INSERT INTO Users (UserId, UserName, Email, FirstName, LastName, ImageUrl, Role, IsActive, CreatedAt)
VALUES ('user-5', 'dangthie', 'dangthie@example.com', N'Đặng Thị', N'E', NULL, 1, 1, '2023-12-31');

INSERT INTO Users (UserId, UserName, Email, FirstName, LastName, ImageUrl, Role, IsActive, CreatedAt)
VALUES ('user-6', 'vuquangf', 'vuquangf@example.com', N'Vũ Quang', N'F', NULL, 1, 1, '2023-12-31');

INSERT INTO Users (UserId, UserName, Email, FirstName, LastName, ImageUrl, Role, IsActive, CreatedAt)
VALUES ('user-7', 'huynhthig', 'huynhthig@example.com', N'Huỳnh Thị', N'G', NULL, 1, 1, '2023-12-31');

INSERT INTO Users (UserId, UserName, Email, FirstName, LastName, ImageUrl, Role, IsActive, CreatedAt)
VALUES ('user-8', 'buihoangh', 'buihoangh@example.com', N'Bùi Hoàng', N'H', NULL, 1, 1, '2023-12-31');

INSERT INTO Users (UserId, UserName, Email, FirstName, LastName, ImageUrl, Role, IsActive, CreatedAt)
VALUES ('user-9', 'doanthi', 'doanthi@example.com', N'Đoàn Thị', N'I', NULL, 1, 1, '2023-12-31');

INSERT INTO Users (UserId, UserName, Email, FirstName, LastName, ImageUrl, Role, IsActive, CreatedAt)
VALUES ('user-10', 'ngocanh', 'ngocanhj@example.com', N'Ngọc Ánh', N'J', NULL, 1, 1, '2023-12-31');

/* 2. Topic */
INSERT INTO Topic (TopicID, Name) 
VALUES ('a1b2c3d4-e5f6-7890-abcd-1234567890ab', N'Giao tiếp hàng ngày');

INSERT INTO Topic (TopicID, Name) 
VALUES ('b2c3d4e5-f678-9012-abcd-2345678901bc', N'Thành ngữ tiếng Anh');

INSERT INTO Topic (TopicID, Name) 
VALUES ('c3d4e5f6-7890-1234-abcd-3456789012cd', N'Ngữ pháp cơ bản');

INSERT INTO Topic (TopicID, Name) 
VALUES ('d4e5f678-9012-3456-abcd-4567890123de', N'Luyện nghe');

INSERT INTO Topic (TopicID, Name) 
VALUES ('e5f67890-1234-5678-abcd-5678901234ef', N'Luyện nói');

/* 3. Courses */
INSERT INTO Courses (CourseID, TopicID, Title, Description, Status, Url, Price, TotalLessons, DifficultyLevel, CreatedAt, UpdatedAt) 
VALUES ('a1b2c3d4-e5f6-7890-abcd-1234567890ab', 'a1b2c3d4-e5f6-7890-abcd-1234567890ab', 
        N'Giao tiếp tiếng Anh cơ bản', 
        N'Khóa học dành cho người mới bắt đầu, giúp bạn tự tin giao tiếp trong các tình huống hàng ngày.', 
        N'Available',
        N'https://learnease.com/basic-english-communication', 
        500000, 0, N'Beginner', '2024-01-01', NULL);

INSERT INTO Courses (CourseID, TopicID, Title, Description, Status, Url, Price, TotalLessons, DifficultyLevel, CreatedAt, UpdatedAt) 
VALUES ('e5f67890-1234-5678-abcd-5678901234ef', 'b2c3d4e5-f678-9012-abcd-2345678901bc', 
        N'Tiếng Anh trung cấp', 
        N'Khóa học nâng cao kỹ năng giao tiếp và sử dụng tiếng Anh ở mức trung cấp.', 
        N'Available',
        N'https://learnease.com/intermediate-english', 
        800000, 0, N'Intermediate', '2024-02-11', NULL);

INSERT INTO Courses (CourseID, TopicID, Title, Description, Status, Url, Price, TotalLessons, DifficultyLevel, CreatedAt, UpdatedAt) 
VALUES ('d4e5f678-9012-3456-abcd-4567890123de', 'c3d4e5f6-7890-1234-abcd-3456789012cd', 
        N'Tiếng Anh nâng cao', 
        N'Khóa học dành cho người sử dụng tiếng Anh thành thạo, tập trung vào các kỹ năng học thuật.', 
        N'No Available',
        N'https://learnease.com/advanced-english', 
        1200000, 0, N'Advanced', '2024-02-11', NULL);

INSERT INTO Courses (CourseID, TopicID, Title, Description, Status, Url, Price, TotalLessons, DifficultyLevel, CreatedAt, UpdatedAt) 
VALUES ('c3d4e5f6-7890-1234-abcd-3456789012cd', 'd4e5f678-9012-3456-abcd-4567890123de', 
        N'Tiếng Nhật sơ cấp', 
        N'Khóa học dành cho người mới học tiếng Anh với các chủ đề giao tiếp cơ bản.', 
        N'Available',
        N'https://learnease.com/basic-japanese', 
        600000, 0, N'Beginner', '2024-02-11', NULL);

INSERT INTO Courses (CourseID, TopicID, Title, Description, Status, Url, Price, TotalLessons, DifficultyLevel, CreatedAt, UpdatedAt) 
VALUES ('b2c3d4e5-f678-9012-abcd-2345678901bc', 'e5f67890-1234-5678-abcd-5678901234ef', 
        N'Tiếng Nhật trung cấp', 
        N'Khóa học tập trung vào ngữ pháp và từ vựng trung cấp tiếng Anh.', 
        N'No Available',
        N'https://learnease.com/intermediate-japanese', 
        900000, 0, N'Intermediate', '2024-02-11', NULL);

/* 3. UserCourses */
INSERT INTO UserCourses (UserCourseID, UserID, CourseID, EnrolledAt, ProgressStatus, ProgressPercentage, CompletedAt) 
VALUES ('a1f2c3d4-e5f6-4789-bcde-1234567890a1', 'user-1', 'a1b2c3d4-e5f6-7890-abcd-1234567890ab', '2024-02-01', N'In Progress', 50, NULL);

INSERT INTO UserCourses (UserCourseID, UserID, CourseID, EnrolledAt, ProgressStatus, ProgressPercentage, CompletedAt) 
VALUES ('b2f3d4e5-f678-4901-bcde-2345678901b2', 'user-2', 'b2c3d4e5-f678-9012-abcd-2345678901bc', '2024-02-03', N'Completed', 100, '2024-02-10');

INSERT INTO UserCourses (UserCourseID, UserID, CourseID, EnrolledAt, ProgressStatus, ProgressPercentage, CompletedAt) 
VALUES ('c3f4e5f6-7890-4123-bcde-3456789012c3', 'user-3', 'c3d4e5f6-7890-1234-abcd-3456789012cd', '2024-02-05', N'In Progress', 30, NULL);

INSERT INTO UserCourses (UserCourseID, UserID, CourseID, EnrolledAt, ProgressStatus, ProgressPercentage, CompletedAt) 
VALUES ('d4f5e678-9012-4345-bcde-4567890123d4', 'user-4', 'd4e5f678-9012-3456-abcd-4567890123de', '2024-02-07', N'Not Started', 0, NULL);

INSERT INTO UserCourses (UserCourseID, UserID, CourseID, EnrolledAt, ProgressStatus, ProgressPercentage, CompletedAt) 
VALUES ('e5f67890-1234-4567-bcde-5678901234e5', 'user-5', 'e5f67890-1234-5678-abcd-5678901234ef', '2024-02-08', N'In Progress', 20, NULL);

INSERT INTO UserCourses (UserCourseID, UserID, CourseID, EnrolledAt, ProgressStatus, ProgressPercentage, CompletedAt) 
VALUES ('f6f78901-2345-4789-bcde-6789012345f6', 'user-6', 'a1b2c3d4-e5f6-7890-abcd-1234567890ab', '2024-02-09', N'Completed', 100, '2024-02-15');

/* 5. Lessons */
INSERT INTO Lessons (LessonID, CourseID, [Index], Title, LessonType, CreatedAt) 
VALUES ('1a2b3c4d-d5e6-4789-bcde-1234567890ab', 'a1b2c3d4-e5f6-7890-abcd-1234567890ab', 1, N'Lời chào cơ bản', 0, '2024-02-01');

INSERT INTO Lessons (LessonID, CourseID, [Index], Title, LessonType, CreatedAt) 
VALUES ('2b3c4d5e-f6e7-4901-bcde-2345678901ac', 'a1b2c3d4-e5f6-7890-abcd-1234567890ab', 2, N'Mẫu câu hàng ngày', 1, '2024-02-02');

INSERT INTO Lessons (LessonID, CourseID, [Index], Title, LessonType, CreatedAt) 
VALUES ('3c4d5e6f-f789-4123-bcde-3456789012ad', 'b2c3d4e5-f678-9012-abcd-2345678901bc', 1, N'Ngữ pháp cơ bản', 1, '2024-02-03');

INSERT INTO Lessons (LessonID, CourseID, [Index], Title, LessonType, CreatedAt) 
VALUES ('4d5e6f70-8901-4345-bcde-4567890123ae', 'b2c3d4e5-f678-9012-abcd-2345678901bc', 2, N'Luyện nói: Giới thiệu bản thân', 3, '2024-02-04');

INSERT INTO Lessons (LessonID, CourseID, [Index], Title, LessonType, CreatedAt) 
VALUES ('5e6f7890-0123-4567-bcde-5678901234af', 'c3d4e5f6-7890-1234-abcd-3456789012cd', 1, N'Thảo luận nâng cao', 3, '2024-02-05');

INSERT INTO Lessons (LessonID, CourseID, [Index], Title, LessonType, CreatedAt) 
VALUES ('6f789012-2345-4789-bcde-6789012345b0', 'c3d4e5f6-7890-1234-abcd-3456789012cd', 2, N'Viết luận nâng cao', 2, '2024-02-06');

INSERT INTO Lessons (LessonID, CourseID, [Index], Title, LessonType, CreatedAt) 
VALUES ('7a890123-3456-4901-bcde-7890123456b1', 'd4e5f678-9012-3456-abcd-4567890123de', 1, N'Giới thiệu bảng chữ cái', 0, '2024-02-07');

INSERT INTO Lessons (LessonID, CourseID, [Index], Title, LessonType, CreatedAt) 
VALUES ('8b901234-4567-4123-bcde-8901234567b2', 'd4e5f678-9012-3456-abcd-4567890123de', 2, N'Luyện phát âm tiếng Anh', 1, '2024-02-08');

INSERT INTO Lessons (LessonID, CourseID, [Index], Title, LessonType, CreatedAt) 
VALUES ('9c012345-5678-4345-bcde-9012345678b3', 'e5f67890-1234-5678-abcd-5678901234ef', 1, N'Đọc hiểu', 2, '2024-02-09');

INSERT INTO Lessons (LessonID, CourseID, [Index], Title, LessonType, CreatedAt) 
VALUES ('0d123456-6789-4567-bcde-0123456789b4', 'e5f67890-1234-5678-abcd-5678901234ef', 2, N'Luyện nghe: Hội thoại', 3, '2024-02-10');

/* 6. Flashcards */
INSERT INTO Flashcards (FlashcardID, LessonID, Front, Back, PronunciationAudioURL, CreatedAt) 
VALUES ('00000000-0000-0000-0000-000000000006', '1a2b3c4d-d5e6-4789-bcde-1234567890ab', N'Car', N'Xe hơi', NULL, '2024-02-01 14:25:00');

INSERT INTO Flashcards (FlashcardID, LessonID, Front, Back, PronunciationAudioURL, CreatedAt) 
VALUES ('00000000-0000-0000-0000-000000000007', '2b3c4d5e-f6e7-4901-bcde-2345678901ac', N'Dog', N'Chó', NULL, '2024-02-02 15:00:00');

INSERT INTO Flashcards (FlashcardID, LessonID, Front, Back, PronunciationAudioURL, CreatedAt) 
VALUES ('00000000-0000-0000-0000-000000000008', '3c4d5e6f-f789-4123-bcde-3456789012ad', N'Computer', N'Máy tính', NULL, '2024-02-03 16:45:00');

INSERT INTO Flashcards (FlashcardID, LessonID, Front, Back, PronunciationAudioURL, CreatedAt) 
VALUES ('00000000-0000-0000-0000-000000000009', '4d5e6f70-8901-4345-bcde-4567890123ae', N'Book', N'Sách', NULL, '2024-02-04 17:30:00');

INSERT INTO Flashcards (FlashcardID, LessonID, Front, Back, PronunciationAudioURL, CreatedAt) 
VALUES ('00000000-0000-0000-0000-000000000010', '5e6f7890-0123-4567-bcde-5678901234af', N'Music', N'Âm nhạc', NULL, '2024-02-05 18:20:00');

/* 7. TheoryLesson */
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

/* 8. VideoLessons */
INSERT INTO VideoLessons (VideoID, LessonID, VideoURL, Duration, CreatedAt) 
VALUES ('f1a2b3c4-d5e6-4789-bcde-1234567890fa', '1a2b3c4d-d5e6-4789-bcde-1234567890ab', 
        'https://example.com/video1', '00:10:00', '2024-02-01');

INSERT INTO VideoLessons (VideoID, LessonID, VideoURL, Duration, CreatedAt) 
VALUES ('f2b3c4d5-f6e7-4901-bcde-2345678901fb', '2b3c4d5e-f6e7-4901-bcde-2345678901ac', 
        'https://example.com/video2', '00:15:00', '2024-02-02');

INSERT INTO VideoLessons (VideoID, LessonID, VideoURL, Duration, CreatedAt) 
VALUES ('f3c4d5e6-f789-4123-bcde-3456789012fc', '3c4d5e6f-f789-4123-bcde-3456789012ad', 
        'https://example.com/video3', '00:20:00', '2024-02-03');

INSERT INTO VideoLessons (VideoID, LessonID, VideoURL, Duration, CreatedAt) 
VALUES ('f4d5e6f7-8901-4345-bcde-4567890123fd', '4d5e6f70-8901-4345-bcde-4567890123ae', 
        'https://example.com/video4', '00:18:00', '2024-02-04');

INSERT INTO VideoLessons (VideoID, LessonID, VideoURL, Duration, CreatedAt) 
VALUES ('f5e6f789-0123-4567-bcde-5678901234fe', '5e6f7890-0123-4567-bcde-5678901234af', 
        'https://example.com/video5', '00:25:00', '2024-02-05');

/* 9. Exercises */
INSERT INTO Exercises (ExerciseID, LessonID, Type, Question, AnswerOptions, CorrectAnswer, CreatedAt) 
VALUES ('f3a7b8c5-10a1-4c35-91b3-d2ab87a48f39', '1a2b3c4d-d5e6-4789-bcde-1234567890ab', 
        N'Trắc nghiệm', N'Câu hỏi 1: Chào hỏi như thế nào?', 
        N'Xin chào; Chào buổi sáng; Tạm biệt', N'Xin chào', '2024-02-01');

INSERT INTO Exercises (ExerciseID, LessonID, Type, Question, AnswerOptions, CorrectAnswer, CreatedAt) 
VALUES ('c8d6e9b2-7865-4d6b-a4b2-56ac3f8d7e1e', '2b3c4d5e-f6e7-4901-bcde-2345678901ac', 
        N'Điền từ', N'Chúng tôi ___ học tiếng Anh.', 
        N'đang; sẽ; không', N'đang', '2024-02-02');

INSERT INTO Exercises (ExerciseID, LessonID, Type, Question, AnswerOptions, CorrectAnswer, CreatedAt) 
VALUES ('b5a0d8c3-7e9a-4fe4-9d4c-1f0a073f0a72', '3c4d5e6f-f789-4123-bcde-3456789012ad', 
        N'Sắp xếp câu', N'Sắp xếp: học / chúng / tiếng / tôi / Anh', 
        N'Tôi học tiếng Anh; Học Anh tiếng tôi; Chúng tôi học tiếng', N'Tôi học tiếng Anh', '2024-02-03');

INSERT INTO Exercises (ExerciseID, LessonID, Type, Question, AnswerOptions, CorrectAnswer, CreatedAt) 
VALUES ('a8e6b4f7-9f10-47e5-b736-11cd82626323', '4d5e6f70-8901-4345-bcde-4567890123ae', 
        N'Trắc nghiệm', N'Câu hỏi 2: Cách giới thiệu bản thân?', 
        N'Tôi tên là ...; Bạn tên là gì?; Chào bạn!', N'Tôi tên là ...', '2024-02-04');

INSERT INTO Exercises (ExerciseID, LessonID, Type, Question, AnswerOptions, CorrectAnswer, CreatedAt) 
VALUES ('d4a99c5d-850d-41a2-9868-0bb567e4a745', '5e6f7890-0123-4567-bcde-5678901234af', 
        N'Điền từ', N'Học tiếng ___ là quan trọng.', 
        N'Anh; Pháp; Đức', N'Anh', '2024-02-05');

/* 10. UserExercises */
INSERT INTO UserExercises (AttemptID, UserID, ExerciseID, UserAnswer, Progress, AttemptAt) 
VALUES ('f1a2d3e4-8b5c-47d8-9a7f-0b6c1a3b4f56', 'user-1', 'f3a7b8c5-10a1-4c35-91b3-d2ab87a48f39', N'Xin chào', N'Completed', '2024-02-01');

INSERT INTO UserExercises (AttemptID, UserID, ExerciseID, UserAnswer, Progress, AttemptAt) 
VALUES ('c2b9d8a3-2f7e-48b1-98b2-04d97b1c9d8d', 'user-2', 'c8d6e9b2-7865-4d6b-a4b2-56ac3f8d7e1e', N'đang', N'In Progress', '2024-02-02');

INSERT INTO UserExercises (AttemptID, UserID, ExerciseID, UserAnswer, Progress, AttemptAt) 
VALUES ('b3e6f7a1-6d1c-4e7e-94a9-d4b1c6a8a7a0', 'user-3', 'b5a0d8c3-7e9a-4fe4-9d4c-1f0a073f0a72', N'Học Anh tiếng tôi', N'Failed', '2024-02-03');

INSERT INTO UserExercises (AttemptID, UserID, ExerciseID, UserAnswer, Progress, AttemptAt) 
VALUES ('d4b8f3a0-9e2b-462f-a02b-1a8c8a3d9e62', 'user-4', 'a8e6b4f7-9f10-47e5-b736-11cd82626323', N'Tôi tên là ...', N'Completed', '2024-02-04');

INSERT INTO UserExercises (AttemptID, UserID, ExerciseID, UserAnswer, Progress, AttemptAt) 
VALUES ('e5f8b6a2-1b8f-4e0b-bf90-3c9a6f9d3b75', 'user-5', 'd4a99c5d-850d-41a2-9868-0bb567e4a745', N'Anh', N'In Progress', '2024-02-05');

/* 11. UserLesson */
INSERT INTO UserLesson 
(ProgressID, UserID, LessonID, Progress, IsVideoCompleted, IsExerciseCompleted, HasAccessedFlashcards, IsTheoryCompleted, StartedAt, CompletedAt, LastAccessedAt)
VALUES 
('A1B2C3D4-E5F6-4789-BCDE-1234567890A1', 'user-1', '1A2B3C4D-D5E6-4789-BCDE-1234567890AB', 85, 1, 0, 1, 1, '2024-02-01T00:00:00Z', NULL, '2024-02-11T00:00:00Z');

INSERT INTO UserLesson 
(ProgressID, UserID, LessonID, Progress, IsVideoCompleted, IsExerciseCompleted, HasAccessedFlashcards, IsTheoryCompleted, StartedAt, CompletedAt, LastAccessedAt)
VALUES 
('B2C3D4E5-F678-4901-BCDE-2345678901B2', 'user-2', '2B3C4D5E-F6E7-4901-BCDE-2345678901AC', 100, 1, 1, 1, 1, '2024-02-01T00:00:00Z', '2024-02-10T00:00:00Z', '2024-02-10T00:00:00Z');

INSERT INTO UserLesson 
(ProgressID, UserID, LessonID, Progress, IsVideoCompleted, IsExerciseCompleted, HasAccessedFlashcards, IsTheoryCompleted, StartedAt, CompletedAt, LastAccessedAt)
VALUES 
('C3D4E5F6-7890-4123-BCDE-3456789012C3', 'user-3', '3C4D5E6F-F789-4123-BCDE-3456789012AD', 70, 1, 0, 1, 0, '2024-02-01T00:00:00Z', NULL, '2024-02-09T00:00:00Z');

INSERT INTO UserLesson 
(ProgressID, UserID, LessonID, Progress, IsVideoCompleted, IsExerciseCompleted, HasAccessedFlashcards, IsTheoryCompleted, StartedAt, CompletedAt, LastAccessedAt)
VALUES 
('D4E5F678-9012-4345-BCDE-4567890123D4', 'user-4', '4D5E6F70-8901-4345-BCDE-4567890123AE', 88, 1, 1, 1, 1, '2024-02-01T00:00:00Z', '2024-02-08T00:00:00Z', '2024-02-08T00:00:00Z');
