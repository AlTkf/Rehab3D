from base64 import encode
import socket
import cv2
import mediapipe as mp

width = 600
height = 600

video = cv2.VideoCapture(0)
video_width = video.set(3, width)
video_height = video.set(4, height)

get_pose = mp.solutions.pose
draw_pose = mp.solutions.drawing_utils

broadcast_coord = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
port = 5052
server = socket.gethostbyname(socket.gethostname())

while True:
    status, image = video.read()

    # convert image from BGR (as captured by cv2) to RGB (as processed by mp)
    image_RGB = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)

    # extract 3D coordinates and visibility of landmarks (points)
    landmarks = get_pose.Pose().process(image_RGB).pose_landmarks

    #print('\n', '#########', '\n')

    # draw landmarks (if any) on image
    if landmarks:
        draw_pose.draw_landmarks(image, landmarks, get_pose.POSE_CONNECTIONS)

        frame_coord = [] 

        for id, data_id in enumerate(landmarks.landmark):

            # landmarks coordinates are expressed as ratios with respect
            # to window dimensions, we want absolute pixel values. we also
            # adjust the value of y for Unity3D
            h, w, c = image.shape
            x, y, z = int(data_id.x*w), int(height-data_id.y*h), int(data_id.z*w)
            frame_coord.append(x)
            frame_coord.append(y)
            frame_coord.append(z)
            #print(id, x, y, z)

        #print('FRAME COORD: ', frame_coord)

    broadcast_coord.sendto(str.encode(str(frame_coord)), (server, port))

    cv2.imshow('Webcam Capture', image)
    cv2.waitKey(1)

    y = input('Do you want to close video capture? (y)')
    if y == 'y':
        cv2.destroyAllWindows()

