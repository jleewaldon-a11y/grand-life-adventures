# Grand Life Adventures - System Documentation

## Core Systems

### 1. Relationship System

**Purpose:** Manage romantic relationships with multiple characters

**Key Features:**
- Track relationship affection (0-100 scale)
- Multiple relationship statuses: Single, Dating, Engaged, Married, Divorced
- Automatic decay when relationships aren't maintained
- Date activities that increase affection
- Marriage and divorce mechanics
- Marriage produces children

**Key Classes:**
- `RelationshipManager` - Main system controller
- `Relationship` - Individual relationship data
- `RelationshipStatus` - Enum for relationship states

### 2. Career System

**Purpose:** Provide career progression and income generation

**Key Features:**
- Multiple career paths (Security, Taxi, Mechanic, Real Estate, Business Owner)
- Experience-based salary progression
- Prestige system reflecting career status
- Automatic paychecks every 7 in-game days
- Career switching

**Career Types:**
- Security Guard (Base: $800, Prestige: 20)
- Taxi Driver (Base: $600, Prestige: 15)
- Mechanic (Base: $1200, Prestige: 40)
- Real Estate Agent (Base: $2000, Prestige: 60)
- Business Owner (Base: $5000, Prestige: 100)

**Key Classes:**
- `CareerSystem` - Main system controller
- `Career` - Career definition

### 3. Family System

**Purpose:** Manage family life, children, and domestic happiness

**Key Features:**
- Up to 4 children per player
- Child development and aging
- Family happiness tracking
- Parenting activities and discipline
- Family bonding increases happiness

**Key Classes:**
- `FamilySystem` - Main system controller
- `Child` - Individual child data

**Child Attributes:**
- Name
- Age (auto-calculated from birth date)
- Happiness (0-100)
- Discipline (0-100)
- Skills (expandable array)

### 4. UI System

**Purpose:** Provide user interface for interacting with all systems

**Key Features:**
- Main menu (F9 to toggle)
- Relationship management interface
- Career selection and info
- Family/children management
- Life status overview

**Key Classes:**
- `UIManager` - Main UI controller
- `MainMenu` - Primary interface
- `RelationshipMenu` - Relationship interactions
- `CareerMenu` - Career management
- `FamilyMenu` - Family management

## Data Flow

```
ModCore (Main Entry Point)
    ↓
    ├─→ RelationshipManager (Maintains relationships)
    ├─→ CareerSystem (Manages career & income)
    ├─→ FamilySystem (Handles family mechanics)
    └─→ UIManager (Displays everything to player)
         ↓
      ConfigManager (Loads/saves settings)
```

## Update Cycle

Each frame:
1. ModCore.OnTick() is called
2. All system Update() methods are called
3. UIManager.Update() renders the interface
4. Player interactions are processed

## Configuration

See `config/config.xml` for all configurable settings including:
- Menu keybind
- Relationship decay rates
- Paycheck intervals
- Maximum children
- Feature toggles
