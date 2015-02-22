import sys
import os
sys.path.append(os.path.dirname(bpy.data.filepath))

from tools_blender import *

import bmesh
import bpy
from random import *
from bpy.props import *
 
#properties of the panel
def initSceneProperties(scn):
	bpy.types.Scene.Level = IntProperty(
		name = "Level", 
		description = "Enter an integer",
		default = 3,
		min = 1,
		max = 6)
	bpy.types.Scene.Height = IntProperty(
		name = "Height", 
		description = "Enter an integer",
		default = 20,
		min = 10,
		max = 30)
	return
initSceneProperties(bpy.context.scene)

#gui panel
class ToolsPanel(bpy.types.Panel):
	bl_label = "Tools For Level Generator"
	bl_space_type = "VIEW_3D"
	bl_region_type = "TOOLS"
 
	def draw(self, context):
		layout = self.layout
		scn = context.scene
		layout.prop(scn, 'Level')
		layout.prop(scn, 'Height')
		layout.operator("level.gen")
		layout.operator("level.delete")
		layout.operator("level.print_index")

class OBJECT_OT_GenButton(bpy.types.Operator):
	bl_idname = "level.gen"
	bl_label = "Gen Level"
	def execute(self, context):
		level = bpy.context.scene.Level
		height = bpy.context.scene.Height
		map(level, height)
		return{'FINISHED'}    

class OBJECT_OT_DeleteButton(bpy.types.Operator):
	bl_idname = "level.delete"
	bl_label = "Delete Level"
	def execute(self, context):
		for ob in bpy.context.scene.objects:
			ob.select = (ob.type == 'MESH' or ob.type == 'LAMP') and (ob.name.startswith("Cube") or ob.name.startswith("Point"))
			bpy.ops.object.delete()
		return{'FINISHED'}    

class OBJECT_OT_PrintButton(bpy.types.Operator):
	bl_idname = "level.print_index"
	bl_label = "Print Index"
	def execute(self, context):
		print_index()
		return{'FINISHED'}    
#registration
bpy.utils.register_module(__name__)

def createMaterial(name):
	img = bpy.data.images.load('//'+name)
	tex = bpy.data.textures.new('TexName', type = 'IMAGE')
	tex.image = img
	mat = bpy.data.materials.new('MatName')
	
	ctex = mat.texture_slots.add()
	ctex.texture = tex
	ctex.texture_coords = 'ORCO'
	ctex.mapping = 'CUBE'
	return mat
	
def setMaterial(ob, mat):
    me = ob.data
    me.materials.append(mat)
	
def map(level, height):
	print("-----Gen Level-----")
	pos_z = 0
	"""rand_level = randint(2,5)
	level = 2"""
	for i in range (1, level + 1):
		create_level(pos_z, i, height)
		pos_z = height * i + 4 * i 
		pos_z += 1

def create_level(pos_z, num_level, height):
	length = randint(30,50)
	width = randint(30,50)
	
	#ground under
	create_ground(pos_z, length, width)
	
	#create points for spawn and teleport
	create_points(pos_z, length, width);
	
	#wall
	create_walls_border(pos_z, length, width, height, 0)
	create_walls_border(pos_z, length, width, height, 1)
	
	border_length = length - 10
	border_width = width - 10
	
	#walls inside
	if length < width:
		wall_length(border_width, border_length, height, pos_z)
	else:
		wall_width(border_width, border_length, height, pos_z)
	
	pos_z = height * num_level + 4 * num_level
	#cap
	create_cap(pos_z, length, width)
	
def create_points(pos_z, length, width):
	bpy.ops.object.lamp_add(type='POINT', radius=1, view_align=False, location=(length - 4, width - 4, pos_z + 2))
	bpy.ops.object.lamp_add(type='POINT', radius=1, view_align=False, location=(4 ,4, pos_z + 2))
	
def wall_width(border_width, border_length, height, pos_z):
	pos_x = uniform(10 , border_length/3)
	pos_y = uniform(border_width/2 , border_width)

	length_wall = uniform(5,border_width - 5)

	create_wall_inside(1, pos_x, pos_y, pos_z, height, length_wall, border_width, 1)

	pos_x2 = uniform(border_length/3 + 10 , border_length * 2/3)
	pos_y2 = uniform(2 , border_width/2)
		
	length_wall = uniform(5,border_width - 5)
	create_wall_inside(1, pos_x2, pos_y2, pos_z, height, length_wall, border_width, 1)

	pos_x3 =	uniform(border_length * 2/3 + 10 , border_length)
	pos_y3 =	uniform(border_width/2 , border_width)
		
	length_wall = uniform(5,border_width - 5)
	create_wall_inside(1, pos_x3, pos_y3, pos_z, height, length_wall, border_width, 1)
	
def wall_length(border_width, border_length, height, pos_z):
	pos_x = uniform(border_length/2 , border_length)
	pos_y = uniform(10 ,border_width/3)

	length_wall = uniform(5,border_length - 5)
	create_wall_inside(2, pos_x, pos_y, pos_z, height, length_wall, border_length, 0)

	pos_x2 = uniform(2 , border_length/2)
	pos_y2 = uniform(border_width/3 + 10 , border_width * 2/3)
		
	length_wall = uniform(5,border_length - 5)
	create_wall_inside(2, pos_x2, pos_y2, pos_z, height, length_wall, border_length, 0)
			
	pos_x3 = uniform(border_length/2 , border_length)
	pos_y3 = uniform(border_width * 2/3 + 10 , border_width)
		
	length_wall = uniform(5,border_length - 5)
	create_wall_inside(2, pos_x3, pos_y3, pos_z, height, length_wall, border_length, 0)
	
def create_wall_inside(index, pos_x, pos_y, pos_z, height, length_wall, border, isWidth):
	mat_wall_inside = createMaterial('wall_inside.jpg')
	bpy.ops.mesh.primitive_cube_add(location=(2,2,pos_z))
	
	wall_inside = bpy.context.active_object
	wall_inside_data = wall_inside.data
	
	bpy.ops.object.mode_set(mode='EDIT')
	mesh = bmesh.from_edit_mesh(wall_inside_data)
	
	if isWidth == 0:
		if length_wall + pos_x > border:
			pos_x = border - length_wall
	else:
		if length_wall + pos_y > border:
			pos_y = border - length_wall
			
	bpy.ops.object.mode_set(mode='OBJECT')
	bpy.ops.transform.translate(value=(pos_x, pos_y, 0), constraint_axis=(True, True, False))
	
	bpy.ops.object.mode_set(mode='EDIT')
	mesh = bmesh.from_edit_mesh(wall_inside_data)
	
	if isWidth == 0:
		extrude_face(mesh, index, length_wall, 0, 0)
	else:
		extrude_face(mesh, index, 0, length_wall, 0)
		
	mesh.faces[5].select = True
	mesh.faces[8].select = True
	extrude_face_simple(mesh, 0, 0, height)
	
	setMaterial(wall_inside, mat_wall_inside)
	bpy.ops.uv.smart_project()
	bpy.ops.object.mode_set(mode='OBJECT')
	bpy.ops.object.shade_smooth()
	
def create_cap(pos_z, length, width):
	mat_cap = createMaterial('cap.jpg')
	
	bpy.ops.mesh.primitive_cube_add(location=(0,0,pos_z))
	cap = bpy.context.active_object
	cap_data = cap.data
	
	bpy.ops.object.mode_set(mode='EDIT')
	mesh = bmesh.from_edit_mesh(cap_data)
	
	extrude_face(mesh, 2, length, 0, 0)
	mesh.faces[1].select = True
	mesh.faces[9].select = True
	extrude_face_simple(mesh, 0, width, 0)
	
	setMaterial(cap, mat_cap)
	bpy.ops.uv.smart_project()
	
	bpy.ops.mesh.subdivide()
	bpy.ops.mesh.subdivide()
	
	bpy.ops.object.mode_set(mode='OBJECT')
	bpy.ops.object.shade_smooth()
	
def create_ground(pos_z, length, width):
	mat_ground = createMaterial('ground.jpg')
	
	bpy.ops.mesh.primitive_cube_add(location=(0,0,pos_z))
	ground = bpy.context.active_object
	ground_data = ground.data
	
	bpy.ops.object.mode_set(mode='EDIT')
	mesh = bmesh.from_edit_mesh(ground_data)
	
	extrude_face(mesh, 2, length, 0, 0)
	mesh.faces[1].select = True
	mesh.faces[9].select = True
	extrude_face_simple(mesh, 0, width, 0)
	
	mesh.faces[1].select = True
	mesh.faces[9].select = True
	bpy.ops.mesh.subdivide()
	bpy.ops.mesh.subdivide()
	bpy.ops.mesh.subdivide()
	bpy.ops.mesh.select_all(action="DESELECT")  
	
	setMaterial(ground, mat_ground)
	bpy.ops.uv.smart_project()
	
	bpy.ops.object.mode_set(mode='OBJECT')
	bpy.ops.object.shade_smooth()
	
def create_walls_border(pos_z, length, width, height, isWidth):
	mat_wall = createMaterial('wall.jpg')

	#create a simple cube and move it to the right place
	bpy.ops.object.mode_set(mode='OBJECT')
	bpy.ops.mesh.primitive_cube_add(location=(0,0,pos_z))
	#bpy.ops.transform.resize(value=(0.3, 0.3, 0.3), constraint_axis=(False, False, False), constraint_orientation='GLOBAL', mirror=False, proportional='DISABLED', proportional_edit_falloff='SMOOTH', proportional_size=1)
	bpy.ops.transform.translate(value=(0, 0, 2), constraint_axis=(False, False, True))
	
	#get data from the cube and set material
	wall = bpy.context.active_object
	wall_data = wall.data
	
	#edit the mesh and transform it in a wall
	bpy.ops.object.mode_set(mode='EDIT')
	mesh = bmesh.from_edit_mesh(wall_data)

	if(isWidth == 0):
		extrude_face(mesh, 2, length, 0, 0)
	else:
		extrude_face(mesh, 1, 0, width, 0)
		
	mesh.faces[5].select = True
	mesh.faces[8].select = True
	extrude_face_simple(mesh, 0, 0, height)
	bpy.ops.object.mode_set(mode='OBJECT')  
	#duplicate this wall and move it
	
	#mat_wall.alpha = 0.5
	#mat_wall.use_transparency = True
	setMaterial(wall, mat_wall)
	bpy.ops.uv.smart_project()
	if(isWidth == 0):
		bpy.ops.object.duplicate_move(OBJECT_OT_duplicate={"linked":False, "mode":'TRANSLATION'}, TRANSFORM_OT_translate={"value":(0, width, 0), "constraint_axis":(False, True, False)});
	else:
		bpy.ops.object.duplicate_move(OBJECT_OT_duplicate={"linked":False, "mode":'TRANSLATION'}, TRANSFORM_OT_translate={"value":(length, 0, 0), "constraint_axis":(True, False, False)});
		
	bpy.ops.object.shade_smooth()

