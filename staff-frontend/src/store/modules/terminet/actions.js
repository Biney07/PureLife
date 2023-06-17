import { getTerminet, getTerminetByDateAndStaff } from "../../../staff-sdk/terminet"

export const fetchTerminetByStaff = ({commit}, staffId) => {
    return new Promise((resolve, reject) => {
        getTerminet(staffId)
        .then((response) => {
            // eslint-disable-next-line no-console
            console.log(response)
            commit('storeTerminet', response.data)
            resolve(response)
        })
        .catch(error => { reject(error) })
    })
};

// export const fetchTerminetByDate = ({commit}, date) => {
//     return new Promise((resolve, reject) => {
//         getTerminetByDate(date)
//         .then((response) => {
//             // eslint-disable-next-line no-console
//             console.log(response)
//             commit('storeTerminet', response.data)
//             resolve(response)
//         })
//         .catch(error => { reject(error) })
//     })
// };

export const fetchTerminetByDateAndStaff = ({commit}, {date, id}) => {
    return new Promise((resolve, reject) => {
        getTerminetByDateAndStaff(date, id)
        .then((response) => {
            // eslint-disable-next-line no-console
            console.log(response)
            commit('storeTerminet', response.data)
            resolve(response)
        })
        .catch(error => { reject(error) })
    })
};

