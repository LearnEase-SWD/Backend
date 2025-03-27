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
VALUES ('user-10', 'ngocanhj', 'ngocanhj@example.com', N'Ngọc Ánh', N'J', NULL, 1, 1, '2023-12-31');

/* 2. Courses */
INSERT INTO Courses (CourseID, Title, Price, TotalLessons, DifficultyLevel, CreatedAt, UpdatedAt) 
VALUES ('a1b2c3d4-e5f6-7890-abcd-1234567890ab', N'Giao tiếp tiếng Anh cơ bản', 500000, 0, N'Beginner', '2024-01-01', NULL);

INSERT INTO Courses (CourseID, Title, Price, TotalLessons, DifficultyLevel, CreatedAt, UpdatedAt) 
VALUES ('b2c3d4e5-f678-9012-abcd-2345678901bc', N'Tiếng Anh trung cấp', 800000, 0, N'Intermediate', '2024-02-11', NULL);

INSERT INTO Courses (CourseID, Title, Price, TotalLessons, DifficultyLevel, CreatedAt, UpdatedAt) 
VALUES ('c3d4e5f6-7890-1234-abcd-3456789012cd', N'Tiếng Anh nâng cao', 1200000, 0, N'Advanced', '2024-02-11', NULL);

INSERT INTO Courses (CourseID, Title, Price, TotalLessons, DifficultyLevel, CreatedAt, UpdatedAt) 
VALUES ('d4e5f678-9012-3456-abcd-4567890123de', N'Tiếng Nhật sơ cấp', 600000, 0, N'Beginner', '2024-02-11', NULL);

INSERT INTO Courses (CourseID, Title, Price, TotalLessons, DifficultyLevel, CreatedAt, UpdatedAt) 
VALUES ('e5f67890-1234-5678-abcd-5678901234ef', N'Tiếng Nhật trung cấp', 900000, 0, N'Intermediate', '2024-02-11', NULL);

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

/* 4. Lessons */
INSERT INTO Lessons (LessonID, CourseID, [Index], Title, LessonType, CreatedAt) 
VALUES ('1a2b3c4d-d5e6-4789-bcde-1234567890ab', 'a1b2c3d4-e5f6-7890-abcd-1234567890ab', 1, N'Chào hỏi cơ bản', N'Video', '2024-02-01');

INSERT INTO Lessons (LessonID, CourseID, [Index], Title, LessonType, CreatedAt) 
VALUES ('2b3c4d5e-f6e7-4901-bcde-2345678901ac', 'a1b2c3d4-e5f6-7890-abcd-1234567890ab', 2, N'Mẫu câu giao tiếp hàng ngày', N'Theory', '2024-02-02');

INSERT INTO Lessons (LessonID, CourseID, [Index], Title, LessonType, CreatedAt) 
VALUES ('3c4d5e6f-f789-4123-bcde-3456789012ad', 'b2c3d4e5-f678-9012-abcd-2345678901bc', 1, N'Ngữ pháp cơ bản', N'Theory', '2024-02-03');

INSERT INTO Lessons (LessonID, CourseID, [Index], Title, LessonType, CreatedAt) 
VALUES ('4d5e6f70-8901-4345-bcde-4567890123ae', 'b2c3d4e5-f678-9012-abcd-2345678901bc', 2, N'Luyện nói: Mô tả bản thân', N'Conversation', '2024-02-04');

INSERT INTO Lessons (LessonID, CourseID, [Index], Title, LessonType, CreatedAt) 
VALUES ('5e6f7890-0123-4567-bcde-5678901234af', 'c3d4e5f6-7890-1234-abcd-3456789012cd', 1, N'Thảo luận nâng cao', N'Conversation', '2024-02-05');

INSERT INTO Lessons (LessonID, CourseID, [Index], Title, LessonType, CreatedAt) 
VALUES ('6f789012-2345-4789-bcde-6789012345b0', 'c3d4e5f6-7890-1234-abcd-3456789012cd', 2, N'Viết luận chuyên sâu', N'Exercise', '2024-02-06');

INSERT INTO Lessons (LessonID, CourseID, [Index], Title, LessonType, CreatedAt) 
VALUES ('7a890123-3456-4901-bcde-7890123456b1', 'd4e5f678-9012-3456-abcd-4567890123de', 1, N'Giới thiệu bảng chữ cái tiếng Nhật', N'Video', '2024-02-07');

INSERT INTO Lessons (LessonID, CourseID, [Index], Title, LessonType, CreatedAt) 
VALUES ('8b901234-4567-4123-bcde-8901234567b2', 'd4e5f678-9012-3456-abcd-4567890123de', 2, N'Phát âm Hiragana và Katakana', N'Theory', '2024-02-08');

INSERT INTO Lessons (LessonID, CourseID, [Index], Title, LessonType, CreatedAt) 
VALUES ('9c012345-5678-4345-bcde-9012345678b3', 'e5f67890-1234-5678-abcd-5678901234ef', 1, N'Đọc hiểu đoạn văn', N'Exercise', '2024-02-09');

INSERT INTO Lessons (LessonID, CourseID, [Index], Title, LessonType, CreatedAt) 
VALUES ('0d123456-6789-4567-bcde-0123456789b4', 'e5f67890-1234-5678-abcd-5678901234ef', 2, N'Luyện nghe hội thoại', N'Conversation', '2024-02-10');

/* 5. Flashcards */
INSERT INTO Flashcards (FlashcardID, LessonID, Front, Back, PronunciationAudioURL, CreatedAt) 
VALUES ('00000000-0000-0000-0000-000000000006', '11111111-1111-1111-1111-111111111111', N'Car', N'Xe hơi', NULL, '2024-01-06 14:25:00');

INSERT INTO Flashcards (FlashcardID, LessonID, Front, Back, PronunciationAudioURL, CreatedAt) 
VALUES ('00000000-0000-0000-0000-000000000007', '22222222-2222-2222-2222-222222222222', N'Dog', N'Chó', NULL, '2024-01-07 15:00:00');

INSERT INTO Flashcards (FlashcardID, LessonID, Front, Back, PronunciationAudioURL, CreatedAt) 
VALUES ('00000000-0000-0000-0000-000000000008', '33333333-3333-3333-3333-333333333333', N'Computer', N'Máy tính', NULL, '2024-01-08 16:45:00');

INSERT INTO Flashcards (FlashcardID, LessonID, Front, Back, PronunciationAudioURL, CreatedAt) 
VALUES ('00000000-0000-0000-0000-000000000009', '44444444-4444-4444-4444-444444444444', N'Book', N'Sách', NULL, '2024-01-09 17:30:00');

INSERT INTO Flashcards (FlashcardID, LessonID, Front, Back, PronunciationAudioURL, CreatedAt) 
VALUES ('00000000-0000-0000-0000-000000000010', '55555555-5555-5555-5555-555555555555', N'Music', N'Âm nhạc', NULL, '2024-01-10 18:20:00');

/* 6. TheoryLesson */
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

/* 7. VideoLessons */
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

/* 8. Exercises */
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

/* 9. UserExercises */
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

/* 10. UserLesson */
INSERT INTO UserLesson (ProgressID, UserID, LessonID, Progress, LastAccessedAt)
VALUES ('a1b2c3d4-e5f6-4789-bcde-1234567890a1', 'user-1', '1a2b3c4d-d5e6-4789-bcde-1234567890ab', 85, '2024-02-11');

INSERT INTO UserLesson (ProgressID, UserID, LessonID, Progress, LastAccessedAt)
VALUES ('b2c3d4e5-f678-4901-bcde-2345678901b2', 'user-2', '2b3c4d5e-f6e7-4901-bcde-2345678901ac', 100, '2024-02-10');

INSERT INTO UserLesson (ProgressID, UserID, LessonID, Progress, LastAccessedAt)
VALUES ('c3d4e5f6-7890-4123-bcde-3456789012c3', 'user-3', '3c4d5e6f-f789-4123-bcde-3456789012ad', 70, '2024-02-09');

INSERT INTO UserLesson (ProgressID, UserID, LessonID, Progress, LastAccessedAt)
VALUES ('d4e5f678-9012-4345-bcde-4567890123d4', 'user-4', '4d5e6f70-8901-4345-bcde-4567890123ae', 88, '2024-02-08');

INSERT INTO UserLesson (ProgressID, UserID, LessonID, Progress, LastAccessedAt)
VALUES ('e5f67890-1234-4567-bcde-5678901234e5', 'user-5', '5e6f7890-0123-4567-bcde-5678901234af', 60, '2024-02-07');

INSERT INTO UserLesson (ProgressID, UserID, LessonID, Progress, LastAccessedAt)
VALUES ('f7890123-2345-4789-bcde-6789012345f6', 'user-6', '6f789012-2345-4789-bcde-6789012345b0', 92, '2024-02-06');

INSERT INTO UserLesson (ProgressID, UserID, LessonID, Progress, LastAccessedAt)
VALUES ('a8901234-3456-4901-bcde-7890123456a7', 'user-7', '7a890123-3456-4901-bcde-7890123456b1', 75, '2024-02-05');

INSERT INTO UserLesson (ProgressID, UserID, LessonID, Progress, LastAccessedAt)
VALUES ('b9012345-4567-4123-bcde-8901234567b8', 'user-8', '8b901234-4567-4123-bcde-8901234567b2', 80, '2024-02-04');

INSERT INTO UserLesson (ProgressID, UserID, LessonID, Progress, LastAccessedAt)
VALUES ('c0123456-5678-4345-bcde-9012345678c9', 'user-9', '9c012345-5678-4345-bcde-9012345678b3', 68, '2024-02-03');

INSERT INTO UserLesson (ProgressID, UserID, LessonID, Progress, LastAccessedAt)
VALUES ('d1234567-6789-4567-bcde-0123456789d0', 'user-10', '0d123456-6789-4567-bcde-0123456789b4', 90, '2024-02-02');

/* 11. UserFlashcard */
INSERT INTO UserFlashcards (UserFlashcardID, UserID, FlashcardID, Progress)
VALUES ('1a2b3c4d-5e6f-4789-bcde-1234567890a1', 'user-1', '00000000-0000-0000-0000-000000000001', 'Beginner');

INSERT INTO UserFlashcards (UserFlashcardID, UserID, FlashcardID, Progress)
VALUES ('2b3c4d5e-6f78-4901-bcde-2345678901b2', 'user-2', '00000000-0000-0000-0000-000000000002', 'Intermediate');

INSERT INTO UserFlashcards (UserFlashcardID, UserID, FlashcardID, Progress)
VALUES ('3c4d5e6f-7890-4123-bcde-3456789012c3', 'user-3', '00000000-0000-0000-0000-000000000003', 'Advanced');

INSERT INTO UserFlashcards (UserFlashcardID, UserID, FlashcardID, Progress)
VALUES ('4d5e6f70-8901-4345-bcde-4567890123d4', 'user-4', '00000000-0000-0000-0000-000000000004', 'Beginner');

INSERT INTO UserFlashcards (UserFlashcardID, UserID, FlashcardID, Progress)
VALUES ('5e6f7890-1234-4567-bcde-5678901234e5', 'user-5', '00000000-0000-0000-0000-000000000005', 'Intermediate');

INSERT INTO UserFlashcards (UserFlashcardID, UserID, FlashcardID, Progress)
VALUES ('6f789012-3456-4789-bcde-6789012345f6', 'user-6', '00000000-0000-0000-0000-000000000006', 'Advanced');

INSERT INTO UserFlashcards (UserFlashcardID, UserID, FlashcardID, Progress)
VALUES ('7a890123-4567-4901-bcde-7890123456e7', 'user-7', '00000000-0000-0000-0000-000000000007', 'Beginner');

INSERT INTO UserFlashcards (UserFlashcardID, UserID, FlashcardID, Progress)
VALUES ('8b901234-5678-4123-bcde-8901234567e8', 'user-8', '00000000-0000-0000-0000-000000000008', 'Intermediate');

INSERT INTO UserFlashcards (UserFlashcardID, UserID, FlashcardID, Progress)
VALUES ('9c012345-6789-4345-bcde-9012345678e9', 'user-9', '00000000-0000-0000-0000-000000000009', 'Advanced');

INSERT INTO UserFlashcards (UserFlashcardID, UserID, FlashcardID, Progress)
VALUES ('0d123456-7890-4567-bcde-0123456789e0', 'user-10', '00000000-0000-0000-0000-000000000010', 'Beginner');
