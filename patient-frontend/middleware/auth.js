import { userExists } from '../helper/auth.js'
export default function (context) {
    if (process.client) {
      if (!userExists()) {
        console.log('User not authorized');
        return {
          redirect: {
            destination: '/login',
            permanent: false
          }
        };
      }
    }
  
    // Continue to the requested page
    return undefined;
  }