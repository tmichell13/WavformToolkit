# Requirements - Waveform Analysis Toolkit

## 1. Overview/Purpose
A C# library and CLI that imports a recorded single-channel vibration file and
reports time-domain statistics (RMS, peak, crest factor) and frequency-domain
results (FFT spectrum, PSD). Modeled on the import → analyze → report pipeline
of commercial vibration-analysis tools.

## 2. Users/Stakeholders
- **Test engineer / analyst** — has a recorded acceleration file and wants key
  metrics and the dominant frequency without opening a full analysis suite.

## 3. Scope
**In scope:** CSV import, statistics, FFT, PSD, console + CSV report, CLI.
**Out of scope:** GUI, multi-channel files, real-time streaming, filtering,
unit conversion.

## 4. Functional Requirements

### FR-01 Import CSV (Must)
*As an analyst, I can import a two-column CSV (time [s], acceleration [g]) so
the data can be analyzed.*
- Given a valid file with uniform time steps, when parsed, then a `Waveform`
  is returned with the correct samples and sample rate (= 1 / Δt).
- Given a file with an optional header row, when parsed, then the header is skipped.

### FR-02 Reject malformed input (Must)
- Given an empty file → error "file is empty".
- Given a header-only file → error "no data rows".
- Given a non-numeric value → error reporting the **line number**.
- Given a row with ≠ 2 columns → error reporting the line number.
- Given a file with < 2 samples → error "not enough data".
- Given non-uniform time steps (beyond tolerance) → error.
- Given a row containing NaN, -Infinity, or Infinity → error reporting the line number.

### FR-03 Time-domain statistics (Must)
- RMS of a constant signal c equals |c|.
- RMS of a sine of amplitude A equals A/√2 (whole number of cycles).
- Peak equals max(|x|).
- Crest factor = Peak / RMS (√2 for a sine; 1 for a constant).

### FR-04 FFT spectrum (Must)
- Single-sided magnitude spectrum from 0 Hz to Nyquist (fs/2).
- Bin spacing = fs / N.
- Given a 50 Hz sine, the dominant frequency is reported as 50 Hz.

### FR-05 Power spectral density (Should)
- One-sided PSD in g²/Hz.
- Σ PSD·Δf equals mean(x²) within tolerance (Parseval).

### FR-06 Report (Must)
- Console table and CSV file output of all results.

### FR-07 CLI (Must)
- `wat analyze <file> [--out <report.csv>] [--top N]`
- Exit codes: 0 success, 1 parse error, 2 invalid arguments. Errors go to stderr.

### FR-08 Stretch (Could)
- WAV (16-bit PCM mono) import; ASCII spectrum bar chart; Welch PSD.

## 5. Non-Functional Requirements
| ID     | Category        | Requirement |
|--------|-----------------|-------------|
| NFR-01 | Accuracy        | Synthetic-signal results match theory within 1e-9 (1e-6 for PSD). |
| NFR-02 | Portability     | Runs on .NET 10 on Windows, Linux, macOS. |
| NFR-03 | Culture safety  | Parsing/formatting is independent of OS locale. |
| NFR-04 | Performance     | Analyzes a 1,000,000-sample file in under 2 s on a typical laptop. |
| NFR-05 | Maintainability | Core library has no console/file I/O; ≥ 90% line coverage. |
| NFR-06 | Documentation   | All public members have XML doc comments; build has 0 warnings. |

## 6. Data/Interface Requirements
- **Input:** UTF-8 CSV, comma-delimited, `.` decimal separator, optional header.
- **Output:** see FR-06 / FR-07.
- 
## 7. Constraints
- C# / .NET 10, Visual Studio 2026, xUnit.
- Solo developer; ~3 half-day iterations.

## 8. Assumptions & Dependencies
- Single channel, uniformly sampled, acceleration in g.
- Files fit in memory.

## 9. Priority
What is most important to deliver first, and what can be deferred or dropped if necessary?
Must/Should/Could/Won't(MoSCoW) prioritization.

## 10. Traceablility
| Req    | Test class                     | Iteration |
|--------|--------------------------------|-----------|
| FR-01  | `WaveformCsvParserTests`       | 1 |
| FR-02  | `WaveformCsvParserTests`       | 1 |
| FR-03  | `StatisticsAnalyzerTests`      | 1 |
| FR-04  | `FftAnalyzerTests`             | 2 |
| FR-05  | `PsdAnalyzerTests`             | 2 |
| FR-06  | `CsvReportWriterTests`         | 3 |
| FR-07  | `CliTests`                     | 3 |

## 11. Glossary
- **Sample rate (fs):** samples per second (Hz).
- **Nyquist frequency:** fs / 2 — highest frequency representable without aliasing.
- **RMS:** √(mean(x²)) — effective signal level.
- **Peak:** max(|x|).
- **Crest factor:** Peak / RMS — how "spiky" a signal is.
- **FFT bin:** one discrete frequency slot, width fs / N.
- **PSD:** power per unit frequency (g²/Hz).

## 12. Open questions
- Crest factor of an all-zero signal: 0, NaN, or error?

## 13. Definition of Done
See [definition-of-done.md](definition-of-done.md).

## 14. Revision History
| Date | Version | Author | Description |
What changed and when
