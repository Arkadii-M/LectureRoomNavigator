import { LectureRoom } from './lectrure-room.dto';
import { NavigationEdge } from './navigation-edge.dto';
import { NavigationNode } from './navigation-node.dto';
export class SimplePath {
  isAnyPathExists: boolean = false;
  navigationNodesArray: NavigationNode[] = [];
  lectureRoomArray: LectureRoom[] = [];
  navigationEdgesArray: NavigationEdge[] = [];
}
