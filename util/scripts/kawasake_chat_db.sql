/*
	Kawasake Database Script
*/

CREATE TABLE UserAccountRole (
	role_id SERIAL PRIMARY KEY NOT NULL,
	name VARCHAR(50) NOT NULL,
	created_at TIMESTAMPTZ NOT NULL DEFAULT(CURRENT_TIMESTAMP),
	deleted_at TIMESTAMPTZ
)

INSERT INTO UserAccountRole (name)
VALUES ('SuperUser'), ('Moderator'), ('User')

CREATE TABLE UserAccount (
	user_id uuid NOT NULL PRIMARY KEY DEFAULT(gen_random_uuid()),
	username VARCHAR(32) NOT NULL,
	password VARCHAR(255) NOT NULL,
	status VARCHAR(255),
	role INT NOT NULL REFERENCES UserAccountRole(role_id) DEFAULT(3),
	created_at TIMESTAMPTZ NOT NULL DEFAULT(CURRENT_TIMESTAMP),
	deleted_at TIMESTAMPTZ
)

CREATE TABLE GroupChat (
	group_id uuid NOT NULL PRIMARY KEY DEFAULT(gen_random_uuid()),
	name VARCHAR(50) NOT NULL,
	invite_code VARCHAR(50) NOT NULL, 
	private BOOLEAN NOT NULL DEFAULT(FALSE),
	password VARCHAR(255),
	created_at TIMESTAMPTZ NOT NULL DEFAULT(CURRENT_TIMESTAMP),
	deleted_at timestamptz
)

CREATE TABLE GroupChatMemberRole (
	role_id serial PRIMARY KEY NOT NULL,
	name VARCHAR(50),
	created_at TIMESTAMPTZ NOT NULL DEFAULT(CURRENT_TIMESTAMP),
	deleted_at timestamptz
)

INSERT INTO GroupChatMemberRole (name)
VALUES ('Admin'), ('Member')

CREATE TABLE GroupChatMember(
	group_id uuid NOT NULL REFERENCES GroupChat(group_id),
	member_id uuid NOT NULL UNIQUE REFERENCES UserAccount(user_id),
	role INT NOT NULL REFERENCES GroupChatMemberRole(role_id) default 2,
	joined_at TIMESTAMPTZ NOT NULL DEFAULT(CURRENT_TIMESTAMP)
)

CREATE TABLE GroupChatMessage(
	group_id uuid NOT NULL REFERENCES GroupChat(group_id),
	member_id uuid NOT NULL REFERENCES UserAccount(user_id),
	message_id serial PRIMARY KEY NOT NULL,
	content text NOT NULL,
	created_at TIMESTAMPTZ NOT NULL DEFAULT(CURRENT_TIMESTAMP)
)
