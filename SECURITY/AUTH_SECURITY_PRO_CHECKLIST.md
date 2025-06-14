# 🔐 AUTH SECURITY CHECKLIST - PRO LEVEL

## 🧼 [1] Input Handling

- [ ] Sanitize all inputs (email, password, query params)
- [ ] Strict schema validation (e.g., using FluentValidation or Zod)
- [ ] Rate limiting login attempts (e.g., 5 per 10 mins)
- [ ] CAPTCHA after X failed attempts

---

## 🧠 [2] Error Messaging

- [ ] Uniform error message (`Invalid email or password`)
- [ ] No disclosure if account exists
- [ ] Log full detail internally (but return generic externally)

---

## ⏱ [3] Timing Attack Protection

- [ ] Constant-time password comparison
- [ ] Dummy hash check if user doesn’t exist
- [ ] Equal response timing across login scenarios

---

## 🧂 [4] Password Hashing

- [ ] Use `argon2id`, `bcrypt`, or `scrypt` with strong cost factors
- [ ] Unique random salt per password
- [ ] Rehash if algorithm/cost updated
- [ ] Enforce password strength (entropy-based, not just regex)

---

## 🔐 [5] Authentication Flow

- [ ] Secure token (JWT/session) signed with strong secret
- [ ] Short access token lifespan (e.g., 15 mins)
- [ ] Refresh token support (rotation on use)
- [ ] Detect and block reused refresh tokens (token theft detection)
- [ ] Bind refresh token to fingerprint (user-agent + IP hash)

---

## 🧱 [6] Session & State Management

- [ ] Regenerate session/token on login
- [ ] Invalidate old tokens on logout/password change
- [ ] Store session tokens securely (e.g., httpOnly, Secure cookie)
- [ ] Enforce single-session-per-device (optional)

---

## 🔁 [7] Password Reset & Recovery

- [ ] One-time reset token (expires in < 15 mins)
- [ ] Reset link expires after first use
- [ ] Notify user on password reset attempt
- [ ] Invalidate all sessions after reset

---

## 🛡 [8] MFA & 2FA

- [ ] TOTP (e.g., Google Authenticator)
- [ ] Backup codes
- [ ] Enforce MFA for admins/high-privilege accounts
- [ ] Optional: Email/SMS OTP (not primary 2FA)

---

## 🔭 [9] Logging & Monitoring

- [ ] Log all login/logout/failure attempts
- [ ] Alert on abnormal patterns (geo, timing, device)
- [ ] Log administrative actions
- [ ] Secure log storage (no PII leakage)

---

## 📦 [10] Security Headers + HTTPS

- [ ] HTTPS-only (with HSTS)
- [ ] CSP, X-Frame-Options, X-XSS-Protection, etc.
- [ ] Disable caching of auth pages (Cache-Control: no-store)
- [ ] Use SameSite cookie policy (Strict or Lax)

---

## 🧠 [11] Advanced Protections

- [ ] Device fingerprinting
- [ ] Geo/IP anomaly detection
- [ ] Behavioral analytics (login speed, mouse movement… etc)
- [ ] Lock sensitive actions behind re-authentication (e.g., change email/password)

---

## 📜 [12] Compliance / Privacy (if applicable)

- [ ] GDPR/CCPA: Right to delete account/data
- [ ] Explicit consent on data collection
- [ ] Minimize data stored in JWT (never store sensitive info)

---

## ⚠️ [13] DON'T EVER:

- [ ] ❌ Store plaintext passwords
- [ ] ❌ Send passwords via email
- [ ] ❌ Use easy security questions
- [ ] ❌ Put user info in localStorage (use httpOnly cookies)
