
# 📡 Beispiel: Paketversand in ein anderes Netzwerk über denselben Router

## 🖥️ Ausgangssituation

| Gerät    | IP-Adresse       | Subnetz          | MAC-Adresse         | Gateway        |
|----------|------------------|------------------|----------------------|----------------|
| PC1      | 192.168.1.100    | 255.255.255.0    | AA-AA-AA-AA-AA-AA   | 192.168.1.1    |
| PC2      | 192.168.2.50     | 255.255.255.0    | BB-BB-BB-BB-BB-BB   | 192.168.2.1    |
| Router   | 192.168.1.1 / 192.168.2.1 | beide Netze | RR-RR-RR-RR-RR-RR   | –              |

---

## 🔄 Beispiel: PC1 sendet ein Ping (ICMP Echo Request) an PC2

### 🕒 Zeitpunkt 1: PC1 erkennt, dass Ziel außerhalb des eigenen Netzes liegt

- Ziel-IP: `192.168.2.50`
- Eigenes Subnetz: `192.168.1.0/24`
- **Entscheidung:** Ziel liegt außerhalb → Paket wird an Gateway `192.168.1.1` gesendet

---

### 🕒 Zeitpunkt 2: PC1 benötigt MAC-Adresse des Gateways

- **ARP Request:**
  ```
  Wer hat 192.168.1.1? Sag es 192.168.1.100
  ```
- **ARP Reply:**
  ```
  192.168.1.1 ist RR-RR-RR-RR-RR-RR
  ```

---

### 🕒 Zeitpunkt 3: PC1 baut das Paket

#### IP-Paket (Layer 3)
| Feld         | Wert               |
|--------------|--------------------|
| Quell-IP     | 192.168.1.100      |
| Ziel-IP      | 192.168.2.50       |
| Protokoll    | ICMP               |

#### Ethernet-Frame (Layer 2)
| Feld         | Wert               |
|--------------|--------------------|
| Quell-MAC    | AA-AA-AA-AA-AA-AA |
| Ziel-MAC     | RR-RR-RR-RR-RR-RR |
| Typ          | IPv4               |

→ **Paket geht an den Router**

---

### 🕒 Zeitpunkt 4: Router prüft das Zielnetz

- Ziel-IP = `192.168.2.50`
- **Router erkennt**: Ziel gehört zu `192.168.2.0/24`
- **Routing vorhanden:** ✅
- → Weiterleitung möglich

---

### 🕒 Zeitpunkt 5: Router kennt MAC-Adresse von PC2 nicht → ARP

- **ARP Request:**
  ```
  Wer hat 192.168.2.50? Sag es 192.168.2.1
  ```
- **ARP Reply:**
  ```
  192.168.2.50 ist BB-BB-BB-BB-BB-BB
  ```

---

### 🕒 Zeitpunkt 6: Router baut neues Frame

#### IP-Paket (Layer 3)
| Quell-IP | 192.168.1.100 |
| Ziel-IP  | 192.168.2.50  |

#### Ethernet-Frame (Layer 2)
| Quell-MAC | RR-RR-RR-RR-RR-RR |
| Ziel-MAC  | BB-BB-BB-BB-BB-BB |

→ **Paket wird an PC2 gesendet**

---

### 🕒 Zeitpunkt 7: PC2 empfängt das Paket

- Erkennt: Das ist für mich
- Antwortet mit ICMP Echo Reply
- Ziel-IP = `192.168.1.100`
- MAC-Adresse des Senders (PC1) wird über ARP ermittelt (falls nötig)

→ Antwort wird über den Router zurück an PC1 gesendet

---

## 🧠 Entscheidungsstellen im Überblick

| Zeitpunkt | Entscheidung                                  |
|-----------|-----------------------------------------------|
| 1         | Ist Ziel im Subnetz? → Nein → Sende an Gateway |
| 2         | MAC-Adresse bekannt? → Nein → ARP an Gateway   |
| 4         | Routing-Eintrag für Zielnetz? → Ja → Weiterleitung |
| 5         | MAC-Adresse des Ziels bekannt? → Nein → ARP     |
| 7         | Bin ich das Ziel? → Ja → Verarbeite Paket       |
