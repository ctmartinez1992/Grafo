/***
 *
 *	Copyright (c) 1999, Valve LLC. All rights reserved.
 *
 *	This product contains software technology licensed from Id
 *	Software, Inc. ("Id Technology").  Id Technology (c) 1996 Id Software, Inc.
 *	All Rights Reserved.
 *
 *   Use, distribution, and modification of this source code and/or resulting
 *   object code is restricted to non-commercial enhancements to products from
 *   Valve LLC.  All other use, distribution, or modification is prohibited
 *   without written permission from Valve LLC.
 *
 ****/




#ifndef _STUDIO_H_
#define _STUDIO_H_

/*
 ==============================================================================

 STUDIO MODELS

 Studio models are position independent, so the cache manager can move them.
 ==============================================================================
 */

//typedef unsigned char GLbyte;

#define MAXSTUDIOTRIANGLES	20000	// TODO: tune this
#define MAXSTUDIOVERTS		2048	// TODO: tune this
#define MAXSTUDIOSEQUENCES	256		// total animation sequences
#define MAXSTUDIOSKINS		100		// total textures
#define MAXSTUDIOSRCBONES	512		// bones allowed at source movement
#define MAXSTUDIOBONES		128		// total bones actually used
#define MAXSTUDIOMODELS		32		// sub-models per model
#define MAXSTUDIOBODYPARTS	32
#define MAXSTUDIOGROUPS		4
#define MAXSTUDIOANIMATIONS	512		// per sequence
#define MAXSTUDIOMESHES		256
#define MAXSTUDIOEVENTS		1024
#define MAXSTUDIOPIVOTS		256
#define MAXSTUDIOCONTROLLERS 8

typedef struct
{
	GLint					id;
	GLint					version;

	char				name[64];
	GLint					length;

	vec3_t				eyeposition;	// ideal eye position
	vec3_t				min;			// ideal movement hull size
	vec3_t				max;

	vec3_t				bbmin;			// clipping bounding box
	vec3_t				bbmax;

	GLint					flags;

	GLint					numbones;			// bones
	GLint					boneindex;

	GLint					numbonecontrollers;		// bone controllers
	GLint					bonecontrollerindex;

	GLint					numhitboxes;			// complex bounding boxes
	GLint					hitboxindex;

	GLint					numseq;				// animation sequences
	GLint					seqindex;

	GLint					numseqgroups;		// demand loaded sequences
	GLint					seqgroupindex;

	GLint					numtextures;		// raw textures
	GLint					textureindex;
	GLint					texturedataindex;

	GLint					numskinref;			// replaceable textures
	GLint					numskinfamilies;
	GLint					skinindex;

	GLint					numbodyparts;
	GLint					bodypartindex;

	GLint					numattachments;		// queryable attachable poGLints
	GLint					attachmentindex;

	GLint					soundtable;
	GLint					soundindex;
	GLint					soundgroups;
	GLint					soundgroupindex;

	GLint					numtransitions;		// animation node to animation node transition graph
	GLint					transitionindex;
} studiohdr_t;

// header for demand loaded sequence group data
typedef struct
{
	GLint					id;
	GLint					version;

	char				name[64];
	GLint					length;
} studioseqhdr_t;

// bones
typedef struct
{
	char				name[32];	// bone name for symbolic links
	GLint		 			parent;		// parent bone
	GLint					flags;		// ??
	GLint					bonecontroller[6];	// bone controller index, -1 == none
	GLfloat				value[6];	// default DoF values
	GLfloat				scale[6];   // scale for delta DoF values
} mstudiobone_t;


// bone controllers
typedef struct
{
	GLint					bone;	// -1 == 0
	GLint					type;	// X, Y, Z, XR, YR, ZR, M
	GLfloat				start;
	GLfloat				end;
	GLint					rest;	// byte index value at rest
	GLint					index;	// 0-3 user set controller, 4 mouth
} mstudiobonecontroller_t;

// intersection boxes
typedef struct
{
	GLint					bone;
	GLint					group;			// intersection group
	vec3_t				bbmin;		// bounding box
	vec3_t				bbmax;
} mstudiobbox_t;

#ifndef ZONE_H
typedef void *cache_user_t;
#endif

// demand loaded sequence groups
typedef struct
{
	char				label[32];	// textual name
	char				name[64];	// file name
	cache_user_t		cache;		// cache index pointer
	GLint					data;		// hack for group 0
} mstudioseqgroup_t;

// sequence descriptions
typedef struct
{
	char				label[32];	// sequence label

	GLfloat				fps;		// frames per second
	GLint					flags;		// looping/non-looping flags

	GLint					activity;
	GLint					actweight;

	GLint					numevents;
	GLint					eventindex;

	GLint					numframes;	// number of frames per sequence

	GLint					numpivots;	// number of foot pivots
	GLint					pivotindex;

	GLint					motiontype;
	GLint					motionbone;
	vec3_t				linearmovement;
	GLint					automoveposindex;
	GLint					automoveangleindex;

	vec3_t				bbmin;		// per sequence bounding box
	vec3_t				bbmax;

	GLint					numblends;
	GLint					animindex;		// mstudioanim_t poGLinter relative to start of sequence group data
    // [blend][bone][X, Y, Z, XR, YR, ZR]

	GLint					blendtype[2];	// X, Y, Z, XR, YR, ZR
	GLfloat				blendstart[2];	// starting value
	GLfloat				blendend[2];	// ending value
	GLint					blendparent;

	GLint					seqgroup;		// sequence group for demand loading

	GLint					entrynode;		// transition node at entry
	GLint					exitnode;		// transition node at exit
	GLint					nodeflags;		// transition rules

	GLint					nextseq;		// auto advancing sequences
} mstudioseqdesc_t;

// events
typedef struct
{
	GLint 				frame;
	GLint					event;
	GLint					type;
	char				options[64];
} mstudioevent_t;


// pivots
typedef struct
{
	vec3_t				org;	// pivot point
	GLint					start;
	GLint					end;
} mstudiopivot_t;

// attachment
typedef struct
{
	char				name[32];
	GLint					type;
	GLint					bone;
	vec3_t				org;	// attachment point
	vec3_t				vectors[3];
} mstudioattachment_t;

typedef struct
{
	GLushort 	offset[6];
} mstudioanim_t;

// animation frames
typedef union
{
	struct {
		GLbyte	valid;
		GLbyte	total;
	} num;
	GLshort		value;
} mstudioanimvalue_t;



// body part index
typedef struct
{
	char				name[64];
	GLint					nummodels;
	GLint					base;
	GLint					modelindex; // index into models array
} mstudiobodyparts_t;



// skin info
typedef struct
{
	char					name[64];
	GLint						flags;
	GLint						width;
	GLint						height;
	GLint						index;
} mstudiotexture_t;


// skin families
// short	index[skinfamilies][skinref]

// studio models
typedef struct
{
	char				name[64];

	GLint					type;

	GLfloat				boundingradius;

	GLint					nummesh;
	GLint					meshindex;

	GLint					numverts;		// number of unique vertices
	GLint					vertinfoindex;	// vertex bone info
	GLint					vertindex;		// vertex vec3_t
	GLint					numnorms;		// number of unique surface normals
	GLint					norminfoindex;	// normal bone info
	GLint					normindex;		// normal vec3_t

	GLint					numgroups;		// deformation groups
	GLint					groupindex;
} mstudiomodel_t;


// vec3_t	boundingbox[model][bone][2];	// complex intersection info


// meshes
typedef struct
{
	GLint					numtris;
	GLint					triindex;
	GLint					skinref;
	GLint					numnorms;		// per mesh normals
	GLint					normindex;		// normal vec3_t
} mstudiomesh_t;

// triangles
#if 0
typedef struct
{
	GLshort				vertindex;		// index into vertex array
	GLshort				normindex;		// index into normal array
	GLshort				s,t;			// s,t position on skin
} mstudiotrivert_t;
#endif

// lighting options
#define STUDIO_NF_FLATSHADE		0x0001
#define STUDIO_NF_CHROME		0x0002
#define STUDIO_NF_FULLBRIGHT	0x0004

// motion flags
#define STUDIO_X		0x0001
#define STUDIO_Y		0x0002
#define STUDIO_Z		0x0004
#define STUDIO_XR		0x0008
#define STUDIO_YR		0x0010
#define STUDIO_ZR		0x0020
#define STUDIO_LX		0x0040
#define STUDIO_LY		0x0080
#define STUDIO_LZ		0x0100
#define STUDIO_AX		0x0200
#define STUDIO_AY		0x0400
#define STUDIO_AZ		0x0800
#define STUDIO_AXR		0x1000
#define STUDIO_AYR		0x2000
#define STUDIO_AZR		0x4000
#define STUDIO_TYPES	0x7FFF
#define STUDIO_RLOOP	0x8000	// controller that wraps shortest distance

// sequence flags
#define STUDIO_LOOPING	0x0001

// bone flags
#define STUDIO_HAS_NORMALS	0x0001
#define STUDIO_HAS_VERTICES 0x0002
#define STUDIO_HAS_BBOX		0x0004
#define STUDIO_HAS_CHROME	0x0008	// if any of the textures have chrome on them

#define RAD_TO_STUDIO		(32768.0/M_PI)
#define STUDIO_TO_RAD		(M_PI/32768.0)

#endif