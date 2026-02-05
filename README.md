# 🌱 Manzili Platform

**Manzili** is a centralized intermediary platform designed to empower home-based businesses and underserved service providers by giving them visibility, structure, and trust — while offering buyers a simple, reliable way to discover and purchase local services and handmade products.

Manzili is not just a marketplace.  
It is a **visibility + empowerment + community platform** aligned with sustainable development goals.

---

## 📌 Table of Contents

- [Vision & Mission](#vision--mission)
- [Problem Statement](#problem-statement)
- [What Makes Manzili Different](#what-makes-manzili-different)
- [Core Features](#core-features)
- [User Roles](#user-roles)
- [Service & Customization Model](#service--customization-model)
- [Order & Payment Philosophy](#order--payment-philosophy)
- [High-Level Architecture](#high-level-architecture)
- [Database Design Philosophy](#database-design-philosophy)
- [Transaction & Escrow Model](#transaction--escrow-model)
- [Order Lifecycle](#order-lifecycle)
- [Security & Trust](#security--trust)
- [Scalability Considerations](#scalability-considerations)
- [Future Roadmap](#future-roadmap)
- [SDGs Alignment](#sdgs-alignment)
- [Contributing](#contributing)
- [License](#license)

---

## 🌍 Vision & Mission

### Vision
To create an inclusive digital ecosystem where home-based businesses can thrive, collaborate, and grow without needing advanced technical or marketing expertise.

### Mission
- Empower underserved sellers with tools for visibility and growth
- Simplify discovery and purchasing for buyers
- Foster trust, community, and economic opportunity
- Support local economies and sustainable entrepreneurship

---

## ❗ Problem Statement

Home-based service providers face:
- Limited marketing knowledge
- Low visibility beyond word-of-mouth
- Fragmented and noisy platforms not built for them
- Difficulty collaborating or scaling

Buyers face:
- Poor discoverability of reliable local services
- Lack of trust and transparency
- Scattered solutions across social media and messaging apps

**Manzili bridges this gap with a single, structured, and supportive platform.**

---

## ✨ What Makes Manzili Different

- Built **specifically** for home-based and underserved sellers
- Supports **customized services**, not just fixed products
- Encourages **collaboration**, not competition
- Escrow-based payments for trust and safety
- Simple, guided UX designed for non-technical users
- Community-first mindset, not pure profit extraction

---

## 🚀 Core Features

### For Sellers
- Create and manage customizable services
- Define structured options with pricing
- Receive and reprice custom requests
- Manage orders and delivery flow
- Gain visibility without marketing expertise
- Collaborate with other providers

### For Buyers
- Discover local services easily
- Customize services to their needs
- Secure payments with escrow protection
- Transparent communication with sellers
- Reviews and trust indicators

### Platform Features
- Multi-seller cart support
- Escrow and delayed payouts
- Commission handling
- Order lifecycle tracking
- Delivery management (internal → external) (in future)
- Review & feedback system

---

## 👥 User Roles

- **Buyer** – discovers, customizes, and purchases services
- **Seller** – home-based provider offering services/products
- **Platform (Manzili)** – intermediary, escrow holder, trust layer
- **Admin** – moderation, dispute handling, system oversight

---

## 🧩 Service & Customization Model

A **Service** on Manzili is a *template*, not a fixed SKU.

Each service:
- Has a base (minimum) price
- Can include seller-defined options (checkboxes with prices)
- May accept free-text customization requests

Buyers can:
- Select predefined options
- Submit detailed custom requests
- Receive repriced offers from sellers

This creates a flow closer to:
> **Request → Agreement → Payment → Fulfillment**

---

## 💳 Order & Payment Philosophy

Manzili uses an **escrow-based model**:

- Buyers pay Manzili, not sellers directly
- Funds are held until delivery is confirmed
- Platform takes a commission (e.g. 5%)
- Sellers are paid after successful fulfillment
- Refunds and cancellations are supported

This ensures:
- Buyer protection
- Seller trust
- Platform accountability

---

## 🏗️ High-Level Architecture

- Frontend: Web platform for buyers and sellers
- Backend: API-driven service handling business logic
- Payment Provider: External (e.g. Stripe)
- Database: Relational, ledger-style transaction model
- Delivery: Internal initially, extensible to external partners

---



## 📒 Transaction & Escrow Model

Manzili uses a **ledger-style Transactions table** with a supporting **Transaction Types** table.

Each transaction represents a single economic event, such as:
- Buyer payment
- Platform escrow credit
- Platform commission
- Seller payout
- Refund

Balances are **derived**, never stored directly.

This design:
- Prevents data corruption
- Supports audits and disputes
- Scales to complex financial flows

---

## 🔄 Order Lifecycle

Typical lifecycle:
1. Buyer adds services to cart
2. Cart splits into seller-specific orders
3. Some orders may require repricing
4. Buyer pays only for ready orders
5. Platform holds funds in escrow
6. Sellers fulfill orders
7. Delivery confirmed
8. Platform releases seller payout
9. Order marked complete

Each step is traceable via transactions.

---

## 🔐 Security & Trust

- Payment provider handles sensitive card data
- Webhooks validate payment authenticity
- Idempotent transaction handling
- Strict order state transitions
- Immutable financial records
- Full audit trail for disputes

---

## 📈 Scalability Considerations

Designed to support:
- Multiple sellers per cart
- High transaction volumes
- Partial failures and retries
- Future wallets and balances
- External delivery providers
- Subscription or promotion models

The architecture avoids premature optimization while remaining future-proof.

---

## 🔮 Future Roadmap

- Mobile applications
- Seller analytics dashboards
- Community collaboration tools
- External delivery integrations
- Seller wallets
- Dispute resolution center
- Subscription & promotion plans
- Advanced recommendation engine

---

## 🌱 SDGs Alignment

Manzili directly supports:

- **SDG 1: No Poverty**  
  By enabling income opportunities for underserved sellers

- **SDG 8: Decent Work & Economic Growth**  
  By promoting entrepreneurship, fair access, and local economies

---

## 🤝 Contributing

Contributions are welcome.

Please:
- Follow clean architecture principles
- Respect immutability of financial data
- Write clear, documented code
- Add tests for critical flows

More contribution guidelines will be added.

---

## 📄 License

This project is licensed under the **MIT License**  
(Subject to change as the project evolves)

---

## 💬 Final Note

Manzili is built with **care, responsibility, and community impact** in mind.  
Every architectural decision prioritizes trust, fairness, and long-term sustainability.

If you’re building with us — welcome.
