SpeedTest Results - Try vs GetPropsInfo
===

Test of typical iproperty GetProperty:

| GetProperty | API | Iterations |
|-------------|-----|------------|
| 189.0 | 184.1 | 500,000 |
| 187.4 | 183.8 | 500,000 |
| 185.3 | 187.9 | 500,000 |
| .061 | .054 | 100 |
| .050 | .068 | 100 |
| .083 | .055 | 100 |
| .093 | .101 | 100 |


Test of GetProperty with a custom iproperty:

| GetProperty | API | Iterations |
|-------------|-----|------------|
| 72.2 | 73.0 | 500,000 |
| 110.0 | 115.1 | 500,000 |
|  |  | 500,000 |
| .043 | .040 | 100 |
| .037 | .026 | 100 |
| .022 | .034 | 100 |


Test of GetProperty where the iproperty does not exist:

| GetProperty | API | Iterations |
|-------------|-----|------------|
| 341.5 |  | 500,000 |
| 342.6 |  | 500,000 |
| 348.8 |  | 500,000 |
| .086 |  | 100 |
| .088 |  | 100 |
| .084 |  | 100 |

