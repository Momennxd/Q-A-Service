# 🧠 Quizz_service

Quizz_service is a backend API for a platform that allows students to **find, organize, and solve question collections** created by teachers and institutions.

## 🚀 Key Features

- 🔍 Search and discover public question collections  
- 🧑‍🏫 Follow trusted creators (teachers / institutions)  
- 📝 Take quizzes and track performance  
- 📚 Build personalized question libraries  
- 🔐 Secure authentication system based on industry best practices  

---

## 🔐 Security & Best Practices

We take security **seriously**.

✅ For full authentication hardening and best practices, please check:  
[📄 AUTH_SECURITY_PRO_CHECKLIST.md](./SECURITY/AUTH_SECURITY_PRO_CHECKLIST.md)

Covers:
- Input validation & sanitization  
- Timing attack defense  
- Token & session management  
- Refresh token rotation  
- MFA & anomaly detection  
- Password hashing (argon2/bcrypt)  
- And more 🔥

---

## 🛠️ Tech Stack

- ASP.NET Core Web API  
- Entity Framework Core  
- SQL Server (or other RDBMS)  
- JWT-based authentication  
- Clean architecture principles  

---

## 📥 Setup

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
