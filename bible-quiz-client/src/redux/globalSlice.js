import {createSlice} from "@reduxjs/toolkit"

const globalSlice = createSlice({
    name: "global",
    initialState: {
        countdown: 0
    },
    reducers:{
        changeCountdownAction: (state, payload) => {
            return{
                ...state,
                countdown: state.countdown = payload
            }
        }
    }
})

export const {changeCountdownAction} = globalSlice.actions;
export default globalSlice.reducer