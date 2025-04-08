# Design Document
Development plan and guide

## Accounts
Simple JWT and refresh token based
approach. An account will have a
username for login and a display name.

## Security
For phase 1, a valid account session
will be enough to do anything on the
site. In the future, a group and role
system might be implemented.

Phase 1 will include a login audit.
A flag will indicate success or
failure.

Regardless of success or failure,
IP address will be recorded.

For success, the user ID will be
added to the audit. For failure,
the input username will be recorded.

Phase 2 will include a banned IP
address system and auto-ban after
a certain amount of failed login
attempts.