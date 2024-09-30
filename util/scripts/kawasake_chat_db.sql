/*
	Kawasake Database Script
*/

CREATE TABLE UserAccountRole (
	role_id SERIAL PRIMARY KEY NOT NULL,
	name VARCHAR(50) NOT NULL,
	created_at TIMESTAMPTZ NOT NULL DEFAULT(CURRENT_TIMESTAMP),
	deleted_at TIMESTAMPTZ
);

INSERT INTO UserAccountRole (name)
VALUES ('SuperUser'), ('Moderator'), ('Common');

CREATE TABLE UserAccount (
	user_id UUID NOT NULL PRIMARY KEY DEFAULT(gen_random_uuid()),
	username VARCHAR(32) NOT NULL,
	avatar_url VARCHAR(255),
	password VARCHAR(255) NOT NULL,
	status VARCHAR(255),
	role INT NOT NULL REFERENCES UserAccountRole(role_id) DEFAULT(3),
	created_at TIMESTAMPTZ NOT NULL DEFAULT(CURRENT_TIMESTAMP),
	deleted_at TIMESTAMPTZ
);

CREATE TABLE GroupChat (
	group_chat_id UUID NOT NULL PRIMARY KEY DEFAULT(gen_random_uuid()),
	owner_id UUID NOT NULL REFERENCES UserAccount(user_id),
	name VARCHAR(50) NOT NULL,
	avatar_url VARCHAR(255),
	invite_code VARCHAR(50) NOT NULL, 
	private BOOLEAN NOT NULL DEFAULT(FALSE),
	password VARCHAR(255),
	created_at TIMESTAMPTZ NOT NULL DEFAULT(CURRENT_TIMESTAMP),
	deleted_at TIMESTAMPTZ 
);

CREATE TABLE GroupChatMemberRole (
	role_id SERIAL PRIMARY KEY NOT NULL,
	name VARCHAR(50),
	created_at TIMESTAMPTZ NOT NULL DEFAULT(CURRENT_TIMESTAMP),
	deleted_at TIMESTAMPTZ 
);

INSERT INTO GroupChatMemberRole (name)
VALUES ('Owner'), ('Admin'), ('Common');

CREATE TABLE GroupChatMember (
	group_chat_member_id SERIAL PRIMARY KEY NOT NULL,
	group_chat_id UUID NOT NULL REFERENCES GroupChat(group_chat_id),
	user_id UUID NOT NULL REFERENCES UserAccount(user_id),
	member_id UUID NOT NULL UNIQUE DEFAULT(gen_random_uuid()),
	avatar_url VARCHAR(255),
	role INT NOT NULL REFERENCES GroupChatMemberRole(role_id) default 2,
	role_granted_by UUID REFERENCES GroupChatMember(member_id),
	joined_at TIMESTAMPTZ NOT NULL DEFAULT(CURRENT_TIMESTAMP)
);

CREATE TABLE GroupChatMessage (
	group_chat_id UUID NOT NULL REFERENCES GroupChat(group_chat_id),
	member_id UUID NOT NULL REFERENCES GroupChatMember(member_id),
	message_id serial PRIMARY KEY NOT NULL,
	content text NOT NULL,
	created_at TIMESTAMPTZ NOT NULL DEFAULT(CURRENT_TIMESTAMP)
);

CREATE TABLE TokenRefresh (
	token_refresh_id SERIAL PRIMARY KEY NOT NULL,
	user_id UUID NOT NULL REFERENCES UserAccount(user_id),
	value TEXT NOT NULL,
	created_at TIMESTAMPTZ NOT NULL DEFAULT(CURRENT_TIMESTAMP),
	expiration TIMESTAMPTZ
);

CREATE TABLE TokenAccess (
	token_access_id SERIAL PRIMARY KEY NOT NULL,
	token_refresh_id INT NOT NULL REFERENCES TokenRefresh(token_refresh_id) ON DELETE CASCADE,
	user_id UUID NOT NULL REFERENCES UserAccount(user_id),
	value TEXT NOT NULL,
	created_at TIMESTAMPTZ NOT NULL DEFAULT(CURRENT_TIMESTAMP),
	expiration TIMESTAMPTZ NOT NULL
);
