<script>
	import { page } from '$app/stores';
	import { onMount } from 'svelte';
	import { get, post } from '$lib/req_utils.js';
	import Modal from '$lib/Modal.svelte';
	import { goto } from '$app/navigation';

	let id = $page.params.id;
	let league;

	async function getLeague(){
		try {
			const json = await get(fetch, `https://localhost:5021/api/Leagues/${id}`);
			if(json.succeeded){
				league = json.data;
			}
		} catch (err){
			console.log(err);
		}
	}

	const nth = function(d) {
		if (d > 3 && d < 21) return 'th';
		switch (d % 10) {
			case 1:  return "st";
			case 2:  return "nd";
			case 3:  return "rd";
			default: return "th";
		}
	}

	const medalClass = function(index){
		switch(index){
			case 0:
				return 'bg-yellow-500 text-yellow-100';
			case 1:
				return 'bg-gray-400 text-gray-100';
			case 2:
				return 'bg-yellow-800 text-yellow-500';
			default:
				return 'text-gray-400 bg-gray-50';
		}
	}

	onMount(getLeague)

	function openLeagueOptions(){
		showLeagueOptionsModal = true;
	}

	function handleToggleModal(){
		showLeagueOptionsModal = !showLeagueOptionsModal;
	}

	let showLeagueOptionsModal = false;
	let leagueOptionsModal;

	async function leaveLeague() {
		const json = await post(fetch, 'https://localhost:5021/api/Leagues/leave', { leagueId: id })

		if(json.succeeded){
			showLeagueOptionsModal = false;
			goto(`/leagues`);
		}

	}

	let inviteLinkText = 'Copy invite code to clipboard';
	let inviteLinkClass = '';
	async function copyInviteCode(){
		await navigator.clipboard.writeText(id);
		inviteLinkText = 'Invite code copied';
		inviteLinkClass = 'text-green-500';
		setTimeout(()=>{
			showLeagueOptionsModal = false;
		},1000);
	}

</script>

{#if league}
	<div class='bg-white rounded-2xl overflow-hidden text-gray-800 w-3/4 lg:w-108 mx-auto shadow-lg'>
		<div class='p-4 text-white bg-blue-500'>
			<h1 class='text-4xl font-bold'>{league.name}</h1>
		</div>

		{#if league.leagueMemberships.length == 0}
			<div class='p-4'>
				<p>No one has joined this league yet. :(</p>
			</div>
		{:else}
			<div class='grid grid-cols-8 divide-y'>
				{#each league.leagueMemberships as membership,index}
					<div class='pl-4 py-4 pr-2  font-semibold text-center text-sm'>
						<span class='mx-auto flex items-center justify-center {medalClass(index)} rounded-full w-8 h-8'>
							{(index+1)}{nth(index+1)}
						</span>
					</div>
					<div class='col-span-5 py-4 px-2 font-semibold'>{membership.name}</div>
					<div class='pl-2 pr-4 py-4'>{membership.points}pts</div>
					<div class='pl-2 pr-4 py-4'>{membership.totalAnswerTime.toFixed(2)}s</div>
				{/each}
			</div>
		{/if}

	</div>
{/if}


<button type='button' class='fixed bottom-5 right-5 w-20 h-20 bg-blue-500 text-white flex items-center justify-center rounded-full' on:click={openLeagueOptions}>
	<svg xmlns="http://www.w3.org/2000/svg" class="h-10 w-10" fill="none" viewBox="0 0 24 24" stroke="currentColor">
		<path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 5v.01M12 12v.01M12 19v.01M12 6a1 1 0 110-2 1 1 0 010 2zm0 7a1 1 0 110-2 1 1 0 010 2zm0 7a1 1 0 110-2 1 1 0 010 2z" />
	</svg>
</button>

<!-- Open Links -->
<Modal
	title=""
	open={showLeagueOptionsModal}
	on:close={handleToggleModal}
	bind:this={leagueOptionsModal}
>
	<svelte:fragment slot="body">
		<div class='grid grid-cols divide-y'>
			<a class='flex cursor-pointer {inviteLinkClass}' on:click={copyInviteCode}>
				<div class='flex-1 p-4 font-semibold'>
					{inviteLinkText}
				</div>
				<div class='flex-0 p-4'>
					<svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
						<path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 16H6a2 2 0 01-2-2V6a2 2 0 012-2h8a2 2 0 012 2v2m-6 12h8a2 2 0 002-2v-8a2 2 0 00-2-2h-8a2 2 0 00-2 2v8a2 2 0 002 2z" />
					</svg>
				</div>
			</a>
			<a class='flex text-red-500 cursor-pointer' on:click={leaveLeague}>
				<div class='flex-1 p-4 font-semibold'>
					Leave League
				</div>
				<div class='flex-0 p-4'>
					<svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
						<path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M17 16l4-4m0 0l-4-4m4 4H7m6 4v1a3 3 0 01-3 3H6a3 3 0 01-3-3V7a3 3 0 013-3h4a3 3 0 013 3v1" />
					</svg>
				</div>
			</a>
		</div>
	</svelte:fragment>
</Modal>