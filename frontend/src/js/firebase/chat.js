// import { ref } from "vue";
// import { firebaseApp } from ".";
// import 'firebase/firestore';

// const firestore = firebaseApp.firestore()
// const messagesCollection = firestore.collection('messages')

// export function useChat(userId) {

//     const messagesQueryBySender = messagesCollection
//         .where('senderId', '==', userId)
//         .orderBy('createdAt', 'desc')
//         .limit(100);

//     const messagesQueryByReceiver = messagesCollection
//         .where('receiverId', '==', userId)
//         .orderBy('createdAt', 'desc')
//         .limit(100);

//     const messages = ref([])

//     Promise.all([
//         messagesQueryBySender.get(),
//         messagesQueryByReceiver.get()
//     ]).then(([senderSnapshot, receiverSnapshot]) => {
//         const allMessages = new Map();
    
//         senderSnapshot.forEach((doc) => {
//             allMessages.set(doc.id, doc.data());
//         });
    
//         receiverSnapshot.forEach((doc) => {
//             allMessages.set(doc.id, doc.data());
//         });
    
//         const sortedMessages = Array.from(allMessages.values()).sort((a, b) => b.createdAt - a.createdAt);
//         messages.value = sortedMessages;
//     }).catch((error) => {
//         console.log("Error getting documents: ", error);
//     });
  
//     const sendMessage = (senderId, receiverId, content) => {
//       messagesCollection.add({
//         senderId,
//         receiverId,
//         content,
//         createdAt: firebase.firestore.FieldValue.serverTimestamp()
//       })
//     }
  
//     return { messages, sendMessage }
//   }
  