# Mango
## 🏗️ Microservices Architecture Diagram

```mermaid
flowchart LR

    %% Client Layer
    A[MVC Application]

    %% Gateway
    B[API Gateway]

    %% Identity
    I[.NET Identity Service :7002]

    %% Core Services
    P[Product Service :7000]
    C[Coupon Service :7001]
    S[Shopping Cart Service :7003]
    O[Order Service :7004]

    %% Service Bus
    SB[(Service Bus)]

    %% Background Services
    E[Email Service]
    R[Rewards Service]

    %% Flow
    A --> B

    B --> P
    B --> C
    B --> S
    B --> O

    %% Identity Connection
    B --> I

    %% Internal Communication
    S --> P
    S --> C

    %% Order Flow
    O --> SB

    %% Event Consumers
    SB --> E
    SB --> R
```

---

## ✨ Highlights

* 🔐 Authentication via **.NET Identity**
* 🚪 Centralized routing using **API Gateway**
* 🔄 Mix of **sync (HTTP)** + **async (Service Bus)** communication
* ⚡ Event-driven design for scalability
* 🧩 Fully decoupled microservices

---
