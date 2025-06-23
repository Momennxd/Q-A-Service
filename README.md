# Q-A Service

**One-line elevator pitch:**  
A flexible Q&A backend platform where creators (teachers, institutions) build “collections” of questions, and consumers (students) practice, compete on leaderboards, and track their progress.

---

## 📄 Table of Contents

1. [Overview](#overview)  
2. [Main Concepts](#main-concepts)  
3. [Features](#features)  
4. [Tech Stack](#tech-stack)  
5. [Getting Started](#getting-started)  
6. [Contributing](#contributing)  

---

## 1. Overview

**What is the Q-A Service?**  
This is a backend service built around three core roles:

- **Creators** (teachers, institutions) who author content  
- **Consumers** (students) who practice and compete  
- **Content** (Collections of Questions)

At its heart, the main content unit is the **Question**, grouped into **Collections**.  Beyond simple Q&A, the system:

- Tracks **submissions** per user & per collection  
- Awards **points** for correct answers  
- Ranks users on **collection-specific** and **global leaderboards**  
- Provides **profiles** & **personal libraries** for every user  
- Supports a **full-text search engine** to find collections, questions, or other users

---

## 2. Main Concepts

### 1 User  
- Represents a person or institution (role: consumer or creator)  
- Holds a profile (name, email, avatar, bio) and a personal library of saved collections  

### 2 Question  
- Text or image prompt + multiple choices  
- At least one correct answer, optional explanation for learning  

### 3 Collection  
- A named group of questions, with description, reviews, likes/dislikes  
- Created & published by a Creator; can be public or private  

### 4 Test  
- A timed version of a Collection (e.g. “15-minute Biology Quiz”)  

### 5 Submission  
- A user’s attempt at a Collection or Test  
- Records which choices were selected, timestamp, and score  
- Supports multiple submissions per Collection for practice  

---

## 3. Features

- **Creator Dashboard**  
  – Create, edit, publish Q&A Collections & Tests  
  – Add explanations, attach images/videos to questions  

- **Consumer Experience**  
  – Browse/purchase or save Collections to your library  
  – Take timed Tests or freeform practice sessions  
  – Track progress across multiple Submissions  

- **Scoring & Leaderboards**  
  – Per-Collection leaderboards showing top scorers  
  – Global leaderboard aggregating all points earned  
  – Rank badges based on total points  

- **Profiles & Social**  
  – User profiles display earned points, badges, reviews  
  – Follow other users, see their public collections  

- **Search Engine**  
  – Full-text search across collections, questions, and users  
  – Filter by subject, creator, tags, difficulty, etc.  

---

## 4. Tech Stack

- **Backend Framework:** ASP .NET Core  
- **Database:** SQL Server  
- **Authentication:** Google OAuth, Telegram login layer  
- **Search:** ElasticSearch (or Azure Search) for full-text filtering  
- **Hosting:** AWS (EC2 / Elastic Beanstalk) for services, RDS for SQL Server  

Optional extensions:
  
- **CI/CD:** GitHub Actions → Docker → AWS ECR/ECS  
- **Monitoring:** CloudWatch, Prometheus + Grafana

---

## 📥 getting-started

1. Clone the repo:
   ```bash
   git clone https://github.com/Momennxd/Q-A-Service.git
   ```

2. Update DB connection in `appsettings.json`

3. Apply migrations:
   ```bash
   dotnet ef database update
   ```

4. Run the API:
   ```bash
   dotnet run
   ```

---

## 🙌 Contributors

- Ahmed  [@AhmedMohammed204 ](https://github.com/AhmedMohammed204)
- Moamen [@Momennxd](https://github.com/Momennxd)  
- Mohamed Hamdy [@MohamedHamdySoftwareEngineer](https://github.com/MohamedHamdySoftwareEngineer)  (Flutter app)
