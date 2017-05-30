## naughty-strings-runner

This software allows you to get the last updated file of the ["Big List of Naughty Strings" repository](https://github.com/minimaxir/big-list-of-naughty-strings). Depending on the user's configurations, each strings will be sent by HTTP request (GET / POST / PUT) to a local application. The results will then be displayed directly in the console.


## Motivation

Covering edge-cases is very important. In order to offer high quality software, this tool should be used systematically before putting any project into production.

## Usage

You can execute the given command to have all possible arguments.

```shell
naughty-strings-runner run --help
```

#### Posssible Arguments:

- `--domain`  ex: `--domain "http://localhost:4000"`
- `--query-param`  ex: `--query-param "productSearch"`

## Installation

Clone the project, compile and just run it using the .EXE file.