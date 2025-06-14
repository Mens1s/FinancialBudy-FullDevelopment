# 📊 Financial Application Features and Progress Tracking (.NET)

## 1️⃣ User Management

- [x] User registration (register)  
- [x] User login (JWT token)  
- [x] View, update, delete user information  
  _Note: Only the user can view their own information_  
- [ ] Password reset (optional)  
- [x] Role-based authorization (admin / regular user)

---

## 2️⃣ Expense Management (Transactions)

- [x] Add, edit, delete, list expenses  
- [x] Categorize expenses (food, transport, rent, etc.)  
- [ ] Expense reports and charts  
- [ ] Expense analysis (AI-supported suggestions, e.g., saving tips)

---

## 3️⃣ Money Transfers (Transfers)

- [x] Transfer between user accounts (currently only one account exists)  
- [x] Transfer to other users  
- [x] isFast check (instant transfer at midnight or with commission)  
- [x] Transfer history and details

---

## 4️⃣ Bank Integrations (Mock API)

- [x] Retrieve credit card debt  
- [x] Retrieve loan debt  
- [ ] Bank balance inquiry (mock API to be written)  
- [ ] Compare loan offers  
- [ ] Compare deposit/investment products  
- [ ] Credit score analysis and suggestions

---

## 5️⃣ Investment and Financial Market Data

- [x] Gold, silver, platinum prices (mock API to be written)  
- [x] BIST100, NASDAQ, DAX index data  
- [x] User portfolio tracking  
- [ ] Investment suggestions (based on risk profile)

---

## 6️⃣ Automatic Payment Orders

- [x] Create automatic payment orders for rent, bills, phone, internet, etc.  
- [x] List, edit, delete payment orders  
- [x] Execute payments at specified times using Hangfire jobs

---

## 7️⃣ Subscription and Bill Tracking

_Subscription management and automatic payment orders are similar, only the domain differs_

- [x] List all user subscriptions (Netflix, Spotify, etc.)  
- [x] Bill tracking screen (electricity, water, gas, internet, etc.)  
- [x] Notifications/reminders for upcoming payment dates  
- [x] Detect and suggest cancellation of unnecessary subscriptions

---

## 8️⃣ Goal-Based Budgeting and Savings Plans

- [x] Create budget plans for user-defined goals (vacation, car, education)  
- [x] Prepare monthly/weekly savings plans  
- [x] Automatic saving rules (e.g., round-up: send change to savings account)

---

## 9️⃣ AI-Supported Financial Suggestions

- [ ] Personal suggestions based on spending habits  
- [ ] Budget overrun warnings  
- [ ] Credit and investment simulations (how actions affect credit score)

---

## 🔄 Background and Technical Operations (with Hangfire)

- [x] Regularly update debt and investment data (e.g., nightly job)  
- [x] Automatic payment and transfer jobs  
- [x] Notification and reminder jobs  
- [x] Database backup/synchronization jobs

---

## 🚧 TODO / Planned Major Improvements

- [ ] A single user can have multiple accounts.  
  _Note: Currently, the architecture is coded for a single account; integration will be done when time allows._  
