# Passmanager
A console app to manage your passwords. App is written using [DPAPI](https://docs.microsoft.com/en-us/dotnet/standard/security/how-to-use-data-protection) and [Spectre.Console](https://spectreconsole.net). This application parses provided commands (args) and allows you to manage your passwords. All passowrds are encrypted using DPAPI and stored in user-scoped [Isolated storage](https://docs.microsoft.com/en-us/dotnet/standard/io/isolated-storage).

## Is this app secure ?
This app is as secure as DPAPI and user scoped Isolated storage are, so if you consider them safe try this app. If you need additional level of security you can apply custom secret to each of the passwords separately. Provided secret value will be used as DPAPI entropy.

## Commands

### Short commands summary:

    - init                Initializes application secure storage, index file
    - purge               Deletes all passwords
    - add <Name>          Adds password entry
    - delete <Query>      Deletes password entry
    - update <Query>      Updates password entry
    - get <Query>         Gets password
    - list                Lists password entries by provided query
    - d-secret <Query>    Deletes custom password secret. Default configuration will be used instead
    - u-secret <Query>    Updates custom password secret
    - stats               Prints some stats about passwords

## More in details about console commands: 

- ### init | Initializes application secure storage, index file

EXAMPLES:

    - init
    - init -f
    - i -f

OPTIONS:

    -h, --help     Prints help information
    -f, --force    The flag to force deletion of existing passwords and index file

- ### purge | Deletes all passwords and index files

EXAMPLES:

    purge
    p

OPTIONS:

    -h, --help    Prints help information

- ### add | Adds password entry

EXAMPLES:

    add example
    add example@example.com -s
    add example --secured

ARGUMENTS:

    <Name>    The name of the password entry

OPTIONS:

    -h, --help       Prints help information
    -s, --secured    An additional custom secret
    -t, --tags       Indicates whether tags will be applied to the entry
 
- ### delete | Deletes password entry
 
EXAMPLES:

    delete
    delete example@gmail.com
    d example@gmail.com

ARGUMENTS:

    <Query>    A query to get password entry

OPTIONS:

    -h, --help    Prints help information

- ### update | Updates password entry
 
EXAMPLES:

    update user@example.com
    u example

ARGUMENTS:

    <Query>    A query to get password entry

OPTIONS:

    -h, --help    Prints help information

- ### get | Gets password

EXAMPLES:

    get example
    g example@example.com

ARGUMENTS:

    <Query>    A query to get password entry

OPTIONS:

    -h, --help    Prints help information
 
- ### list | Lists password entries by provided query
 
EXAMPLES:
 
    list
    l example

ARGUMENTS:

    [Query]    A query to get password entries

OPTIONS:

    -h, --help    Prints help information
    -t, --tags    Groups all passwords by t
 
- ### d-secret | Deletes custom password secret. Password entry will be encrypted using default app configuration
 
EXAMPLES:

    d-secret
    d-secret example@gmail.com
    ds example

ARGUMENTS:

    <Query>    A query to get password entry

OPTIONS:

    -h, --help    Prints help information
 
- ### u-secret | Updates custom password secret

EXAMPLES:

    u-secret user@example.com
    us example

ARGUMENTS:

    <Query>    A query to get password entry

OPTIONS:

    -h, --help    Prints help information
    
- ### stats | Prints some stats about passwords

EXAMPLES:

    stats
    s

OPTIONS:

    -h, --help    Prints help information
