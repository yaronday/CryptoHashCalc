# CryptoHashCalc

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
![Platform](https://img.shields.io/badge/platform-Windows-blue)
![Framework](https://img.shields.io/badge/.NET-Framework%204.7.2-lightgrey)
![Language](https://img.shields.io/badge/language-C%23-178600)

**CryptoHashCalc** is a Windows-based file hashing utility designed to compute cryptographic digests and checksums from the command line or via Windows Explorer’s context menu.  
It supports SHA-2, SHA-3 (BouncyCastle), MD5, and CRC32 for integrity verification.

CryptoHashCalc is implemented as a WinForms application targeting **.NET Framework 4.7.2** and distributed under the **MIT License**.

---

## Features

- Support for the following algorithms:
  - CRC32
  - MD5
  - SHA-256 / SHA-384 / SHA-512
  - SHA3-256 / SHA3-384 / SHA3-512
- Command-line interface for scripting and automation:
  - `CryptoHashCalc.exe <file_path> <algorithm>`
- Optional Windows Explorer context menu integration via registry files
- Simple and focused UI:
  - Displays hash results clearly
  - One-click copy to clipboard
- Lightweight, dependency-minimal distribution
- MIT licensed

---

## Supported Algorithms

| Algorithm   | Digest Size (bits) | Category                | Implementation Source            |
|------------|--------------------|-------------------------|----------------------------------|
| CRC32      | 32                 | Checksum (non-crypto)   | Custom `Crc32` implementation    |
| MD5        | 128                | Cryptographic (legacy)  | `System.Security.Cryptography`   |
| SHA-256    | 256                | Cryptographic (SHA-2)   | `System.Security.Cryptography`   |
| SHA-384    | 384                | Cryptographic (SHA-2)   | `System.Security.Cryptography`   |
| SHA-512    | 512                | Cryptographic (SHA-2)   | `System.Security.Cryptography`   |
| SHA3-256   | 256                | Cryptographic (SHA-3)   | BouncyCastle (`Sha3Digest`)      |
| SHA3-384   | 384                | Cryptographic (SHA-3)   | BouncyCastle (`Sha3Digest`)      |
| SHA3-512   | 512                | Cryptographic (SHA-3)   | BouncyCastle (`Sha3Digest`)      |

> **Note:** CRC32 is not cryptographically secure; it is provided for compatibility and high-speed integrity checking.

---

## Command-Line Usage

### Syntax

```text
CryptoHashCalc.exe <file_path> <algorithm>


### Example

```text
CryptoHashCalc.exe "C:\Data\disk_image.iso" SHA256
```

On completion, the application displays:

* File name
* Selected algorithm
* Resulting hash (hex)
* Option to copy the value to the clipboard

### Error Conditions

The application reports:

* Missing or invalid arguments
* Unsupported algorithm name
* Missing BouncyCastle library for SHA-3
* File read or access errors

---

## Windows Explorer Context Menu Integration

CryptoHashCalc may optionally be integrated into the Windows Explorer context menu through provided registry scripts.

### Installation (`CHC.reg`)

Adds the following submenu:

```
Crypto Hash Calc >
    CRC32
    MD5
    SHA256
    SHA384
    SHA512
    SHA3-256
    SHA3-384
    SHA3-512
```

Selecting any entry launches the application with the chosen algorithm.

### Removal (`CHCremove.reg`)

Removes the entire submenu and associated commands.

---

## Deployment

A deployment script is included to copy required files from the Release build output directory to a target installation directory.

Typical runtime layout:

```text
CryptoHashCalc.exe
BouncyCastle.Crypto.dll
```

No installer is required.

---

## System Requirements

* Windows 10 or later
  (Compatible with Windows 7/8 if .NET Framework 4.7.2 is installed)
* .NET Framework 4.7.2
* `BouncyCastle.Crypto.dll` located next to the executable for SHA-3 support

---

## Versioning

CryptoHashCalc uses semantic versioning:

* **MAJOR** – breaking changes
* **MINOR** – backward-compatible enhancements
* **PATCH** – fixes and small refinements

Version metadata is defined in:

```
Properties/AssemblyInfo.cs
```

---

## License

This project is licensed under the **MIT License**.
See the [`LICENSE`](LICENSE) file for details.

**Copyright (c) 2025
Yaron Dayan**

---

